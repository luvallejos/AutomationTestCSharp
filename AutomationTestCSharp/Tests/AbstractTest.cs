using AutomationTestCSharp.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using UITestFramework.Dto;
using UITestFramework.Utilities;

namespace AutomationExercise.Tests
{
    public abstract class AbstractTest
    {
        #region Fields
        private const string _defaultProtocol = "https";
        private const string _defaultBaseUrl = "automationexercise.com";
        private static readonly object _lock = new object();
        public UserData userData;
        public const string baseURL = @"https://automationexercise.com";
        public APIClientHelper apiHelper;
        public IWebDriver driver;
        #endregion

        #region Methods
        public IWebDriver Initialize()
        {
            //Get environment settings
            var envDataFile = ResourcesUtilities.GetEmbeddedResourceFromCommon("EnvData", $"{ConfigurationManager.AppSettings["TargetEnvironment"]}.json");
            var envData = ResourcesUtilities.DeserializeObject<EnvironmentData>(envDataFile);

            //Get user settings
            var userDataFile = ResourcesUtilities.GetEmbeddedResourceFromCommon("UserData", $"{ConfigurationManager.AppSettings["User"]}.json");
            userData = ResourcesUtilities.DeserializeObject<UserData>(userDataFile);

            string url = SetUrl(envData);

            return InitBrowser(url);
        }

        private string SetUrl(EnvironmentData envData)
        {
            // Create the root url   
            return new UriBuilder
            {
                Scheme = envData.DefaultProtocol != null ? envData.DefaultProtocol : _defaultProtocol,

                Host = envData.BaseUrl != null ? envData.BaseUrl : _defaultBaseUrl,
            }.ToString();
        }

        public static IEnumerable<object[]> GetDataRow(string dataSourceName)
        {
            lock (_lock)
            {
                DataDrivenManage dataDriven = new DataDrivenManage();
                var testcasesData = dataDriven.TestCases(dataSourceName)
                    .Cast<Dictionary<string, object>>()
                    .ToList();

                foreach (var row in testcasesData)
                {
                    var dict = row.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.ToString() ?? string.Empty
                    );

                    yield return new object[] { dict };
                }
            }
        }

        private IWebDriver InitBrowser(string url)
        {
            // Set ChromeDriver options (optional)
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized"); // open browser in maximized mode
            options.AddArgument("--disable-infobars"); // disabling infobars
            options.SetLoggingPreference(LogType.Browser, LogLevel.All); //saving logs for browser

            driver = new ChromeDriver(options);
            driver.Manage().Window.Size = new Size(1920, 1080);
            driver.RemoveAds();

            //Navigate to Base Url
            driver.Navigate().GoToUrl(url);

            try
            {
                 // Handle consent pop-up
                 WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                 wait.Until(d => driver.FindElement(By.Id("fc-focus-trap-pre-div")) != null);
                driver.FindElement(By.CssSelector("button[aria-label=Consent]")).Click();
            }
            catch (Exception)
            {
                //catch any exception if the consent pop-up is not found
            }
            return driver;
        }

        public void Cleanup(IWebDriver driver)
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;

                if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    var testName = TestContext.CurrentContext.Test.Name;
                    var safeName = string.Join("_", testName.Split(Path.GetInvalidFileNameChars()));

                    var root = Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts");

                    TestLoggerHelper.SaveOnFailure(driver, root);

                    TestContext.AddTestAttachment(Path.Combine(root, "currentUrl.txt"));
                    TestContext.AddTestAttachment(Path.Combine(root, "browserConsole.log"));
                    TestContext.AddTestAttachment(Path.Combine(root, "pageSource.html"));
                }
            }
            finally
            {
                try { driver?.Quit(); } catch {}
            }
        }

        #endregion
    }
}

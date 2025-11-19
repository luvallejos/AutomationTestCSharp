using AutomationTestCSharp.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
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


            string ChromeDriver = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            driver = new ChromeDriver(ChromeDriver, options);

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
            driver.Quit();
        }

        #endregion
    }
}

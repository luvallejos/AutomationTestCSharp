using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;

namespace AutomationTestCSharp.Utilities
{
    public static class TakingScreenshotHelper
    {
        public static void TakeScreenshotIfTestFailed(this IWebDriver driver)
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != NUnit.Framework.Interfaces.TestStatus.Failed)
                return;

            string rawTestName = TestContext.CurrentContext.Test.Name;
            var safeTestName = SanitizeFileName(rawTestName);

            var fileName = $"{safeTestName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

            var screenshotsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var fullPath = Path.Combine(screenshotsDir, fileName);

            try 
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath, ScreenshotImageFormat.Png);

                TestContext.WriteLine($"Screenshot guardada en: {fullPath}");
                TestContext.AddTestAttachment(fullPath, "Screenshot al fallar");
            }
            catch (Exception) 
            { 

            }
        }

        private static string SanitizeFileName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return string.Concat(name.Select(ch => invalid.Contains(ch) ? '_' : ch));
        }
    }
}

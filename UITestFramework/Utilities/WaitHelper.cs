using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace UITestFramework.Utilities
{
    public static class WaitHelper
    {
        private static WebDriverWait _wait;

        public static void WaitUntilDisplayed(this IWebDriver driver, IWebElement e, string errorMessage)
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            try
            {
                _wait.Until(drv => e.Displayed);
            }
            catch (Exception)
            {
                throw new WebDriverTimeoutException(errorMessage);
            }
        }
    }
}

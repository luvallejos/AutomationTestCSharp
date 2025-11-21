using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace UITestFramework.Utilities
{
    public static class WaitHelper
    {
        private static WebDriverWait _wait;

        public static void WaitUntilDisplayed(this IWebDriver driver, IWebElement e, string errorMessage)
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                _wait.Until(drv => e.Displayed);
            }
            catch (Exception)
            {
                throw new WebDriverTimeoutException(errorMessage);
            }
        }

        public static void WaitUntilDisplayed(this IWebDriver driver, string locator, string errorMessage)
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(locator)));
            }
            catch (Exception)
            {
                throw new WebDriverTimeoutException(errorMessage);
            }
        }
    }
}

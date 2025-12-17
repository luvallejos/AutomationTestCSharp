using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace UITestFramework.Utilities
{
    public static class WaitHelper
    {
        private static WebDriverWait _wait;

        public static IWebElement WaitUntilVisible(this IWebDriver driver, By locator, string message = "")
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            return _wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    throw new NoSuchElementException(message);
                }
                catch (StaleElementReferenceException)
                {
                    throw new StaleElementReferenceException(message);  
                }
            });
        }
    }
}

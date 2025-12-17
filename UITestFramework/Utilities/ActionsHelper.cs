using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace UITestFramework.Utilities
{
    public static class ActionsHelper
    {
        public static void ScrollToElement(this IWebDriver driver, By locator)
        {
            var element = driver.WaitUntilVisible(locator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void Type(this IWebDriver driver, By locator, string text)
        {
            var element = driver.WaitUntilVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public static void Select(this IWebDriver driver, By locator, string text)
        {
            var element = driver.WaitUntilVisible(locator);
            element.SendKeys(text);
        }

        public static void Click(this IWebDriver driver, By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                var element = wait.Until(SeleniumExtras.WaitHelpers
                    .ExpectedConditions.ElementToBeClickable(locator));

                ScrollToCenter(driver, element);
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                WaitForOverlaysToDisappear(driver);

                var element = wait.Until(SeleniumExtras.WaitHelpers
                    .ExpectedConditions.ElementToBeClickable(locator));

                ScrollToCenter(driver, element);

                try
                {
                    element.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    JsClick(driver, element);
                }
            }
        }

        private static void ScrollToCenter(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center', inline:'center'});",
                element
            );
        }

        private static void JsClick(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver)
                .ExecuteScript("arguments[0].click();", element);
        }

        public static void WaitForOverlaysToDisappear(IWebDriver driver)
        {
            var overlays = new[]
            {
        By.CssSelector(".modal-backdrop"),
        By.CssSelector(".overlay"),
        By.CssSelector(".loading"),
        By.CssSelector(".spinner")
        };
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            foreach (var overlay in overlays)
            {
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers
                        .ExpectedConditions.InvisibilityOfElementLocated(overlay));
                }
                catch (WebDriverTimeoutException)
                {
                }
            }
        }

        public static void HoverElement(this IWebDriver driver, IWebElement element)
        {
            var actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
        }

        public static void RemoveAds(this IWebDriver driver)
        {
            try
            {
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(@"
                                const selectors = [
                                  'iframe[id^=""google_ads""]',
                                  'ins.adsbygoogle',
                                  'ins[class~=""adsbygoogle""]',
                                  'div[id*=""google_ads""]',
                                  'div[id*=""ad-position""]',
                                  'div[id*=""ad-position""] iframe'
                                ];
                                selectors.forEach(sel => {
                                    document.querySelectorAll(sel).forEach(e => e.remove());
                                });"
            );
            }
            catch
            {
            }
        }
    }
}

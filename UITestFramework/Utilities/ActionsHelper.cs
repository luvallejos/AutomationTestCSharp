using OpenQA.Selenium;

namespace UITestFramework.Utilities
{
    public static class ActionsHelper
    {
        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
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

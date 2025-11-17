using OpenQA.Selenium;

namespace UITestFramework.Utilities
{
    public static class ActionsHelper
    {
        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}

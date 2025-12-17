using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using UITestFramework.Utilities;

namespace UITestFramework.Pages.Commons
{
    public class BreadCrumbNavigation
    {
        #region Private Variables
        protected readonly IWebDriver _driver;
        private static readonly By BreadCrumb = By.CssSelector("div.breadcrumbs");
        private const string _breadCrumbNavigationListLocator = "li";
        #endregion

        #region Constructors
        public BreadCrumbNavigation(IWebDriver webDriver)
        {
            _driver = webDriver;
        }
        #endregion

        #region Methods
        public List<string> GetNavigationBreadcrumsList()
        {
            List<string> nav = new List<string>();
            var el = _driver.WaitUntilVisible(BreadCrumb);
            List<IWebElement> elements = el.FindElements(By.CssSelector(_breadCrumbNavigationListLocator)).ToList();

            foreach (var element in elements)
            {
                var a = element.FindElements(By.CssSelector("a")).FirstOrDefault();
                if (a != null)
                {
                     nav.Add(a.Text.ToPlainText().ToLower());
                } else
                {
                    nav.Add(element.Text.ToPlainText().ToLower());
                }
            }

            return nav;   
        }

        public void ValidateNavigationBreadcrumb(string navigationItems) 
        {
            List<string> nav = GetNavigationBreadcrumsList();
            var items = navigationItems.Split(',');

            foreach(var item in items)
            {
                ClassicAssert.IsTrue(nav.Contains(item.ToLower()), $"Error in Navigation Breadcrumbs. Expected Navigation Items: {navigationItems}, but navigation items found: {nav.ToArray().ToString()}");
            }
        }

        #endregion
    }
}

using OpenQA.Selenium;
using UITestFramework.Pages.Common;
using UITestFramework.Pages.Commons;

namespace UITestFramework.Pages
{
    public class BasePage
    {
        #region Constants
        public readonly IWebDriver _driver;
        #endregion

        #region Properties
        public Header Header { get; private set; }
        public SideBar SideBar { get; private set; }
        public FeaturedItems FeaturedItems { get; private set; }
        public AddedToCartModal AddedToCartModal { get; private set; }
        #endregion

        #region Constructors
        public BasePage(IWebDriver webDriver)
        {
            this._driver = webDriver;
            Header = new Header(webDriver);
            SideBar = new SideBar(webDriver);
            FeaturedItems = new FeaturedItems(webDriver);
            AddedToCartModal = new AddedToCartModal(webDriver);
        }

        #endregion
    }
}

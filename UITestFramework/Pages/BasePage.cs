using OpenQA.Selenium;
using UITestFramework.Pages.Common;
using UITestFramework.Pages.Commons;

namespace UITestFramework.Pages
{
    public abstract class BasePage
    {
        #region Constants
        protected readonly IWebDriver _driver;
        #endregion

        #region Properties
        public Header Header { get; private set; }
        public SideBar SideBar { get; private set; }
        public FeaturedItems FeaturedItems { get; private set; }
        public AddedToCartModal AddedToCartModal { get; private set; }
        #endregion

        #region Constructors
        protected BasePage(IWebDriver webDriver)
        {
            _driver = webDriver;
            Header = new Header(webDriver);
            SideBar = new SideBar(webDriver);
            FeaturedItems = new FeaturedItems(webDriver);
            AddedToCartModal = new AddedToCartModal(webDriver);
        }

        #endregion
    }
}

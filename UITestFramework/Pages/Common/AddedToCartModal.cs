using OpenQA.Selenium;
using System.Threading;
using UITestFramework.Utilities;

namespace UITestFramework.Pages.Common
{
    public class AddedToCartModal
    {
        #region Private Variables
        protected readonly IWebDriver _driver;
        private static readonly By CartModal = By.CssSelector("#cartModal[class='modal show']");
        private static readonly By GoToViewCartBtn = By.CssSelector("a[href='/view_cart']");
        private static readonly By ContinueShoppingBtn = By.CssSelector("button[class~='close-modal']");
        #endregion

        #region Constructors
        public AddedToCartModal(IWebDriver webDriver)
        {
            _driver = webDriver;
        }
        #endregion

        #region Methods
        public bool IsCartModalDisplayed()
        {
            try
            {
                var el = _driver.WaitUntilVisible(CartModal);
                return el.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public ViewCartPage GoToViewCartPage() 
        {
            _driver.ScrollToElement(CartModal);
            _driver.Click(GoToViewCartBtn);
            ViewCartPage viewCartPage = new ViewCartPage(_driver);
            viewCartPage.waitUntilViewCartPageDisplayed();
            return viewCartPage;
        }

        public void ClickContinueShopping()
        {
            _driver.ScrollToElement(CartModal);
            _driver.Click(ContinueShoppingBtn);
            Thread.Sleep(3000); //wait for modal to disappear
        }
        #endregion
    }
}

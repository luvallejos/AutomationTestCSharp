using OpenQA.Selenium;
using System.Threading;
using UITestFramework.Utilities;

namespace UITestFramework.Pages.Common
{
    public class AddedToCartModal
    {
        #region Constants
        private readonly IWebDriver _driver;
        private const string _cartModalDisplayedLocator = "#cartModal[class='modal show']";
        private const string _goToViewCartBtnLocator = "a[href='/view_cart']";
        private const string _continueShoppingBtnLocator = "button[class~='close-modal']";

        #endregion

        #region Properties
        public IWebElement CartModal => _driver.FindElement(By.CssSelector(_cartModalDisplayedLocator));
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
                _driver.WaitUntilDisplayed(_cartModalDisplayedLocator, "Added to cart modal is not displayed");
                return CartModal.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public ViewCartPage GoToViewCartPage() 
        {
            _driver.ScrollToElement(CartModal);
            CartModal.FindElement(By.CssSelector(_goToViewCartBtnLocator)).Click();
            ViewCartPage viewCartPage = new ViewCartPage(_driver);
            viewCartPage.waitUntilViewCartPageDisplayed();
            return viewCartPage;
        }

        public void ClickContinueShopping()
        {
            _driver.ScrollToElement(CartModal);
            CartModal.FindElement(By.CssSelector(_continueShoppingBtnLocator)).Click();
            Thread.Sleep(3000); //wait for modal to disappear
        }
        #endregion
    }
}

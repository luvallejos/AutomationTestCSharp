using OpenQA.Selenium;
using UITestFramework.Utilities;


namespace UITestFramework.Pages.Commons
{
    public class Header
    {
        #region Constants
        private readonly IWebDriver _driver;
        private const string _homeBtnLocator = "#header i.fa.fa-home";
        private const string _productsBtnLocator = "#header i.material-icons.card_travel";
        private const string _cartBtnLocator = "#header i.fa.fa-shopping-cart";
        private const string _singUpLoginBtnLocator = "#header i.fa.fa-lock";
        private const string _deleteAccountBtnLocator = "#header i.fa-trash-o";
        private const string _logOutUserBtnLocator = "#header i.fa.fa-lock";

        #endregion

        #region Properties
        public IWebElement HomeBtn => _driver.FindElement(By.CssSelector(_homeBtnLocator));
        public IWebElement ProductsBtn => _driver.FindElement(By.CssSelector(_productsBtnLocator));
        public IWebElement CartBtn => _driver.FindElement(By.CssSelector(_cartBtnLocator));
        public IWebElement SignUpLoginBtn => _driver.FindElement(By.CssSelector(_singUpLoginBtnLocator));
        public IWebElement DeleteAccountBtn => _driver.FindElement(By.CssSelector(_deleteAccountBtnLocator));
        public IWebElement LogOutBtn => _driver.FindElement(By.CssSelector(_logOutUserBtnLocator));

        #endregion

        #region Constructors
        public Header(IWebDriver webDriver)
        {
            this._driver = webDriver;
        }

        #endregion

        #region Methods
        public HomePage GoToHomePage()
        {
            _driver.ScrollToElement(HomeBtn);
            HomeBtn.Click();
            HomePage homePage = new HomePage(_driver);
            homePage.waitUntilHomePageDisplayed();
            return homePage;
        }

        public ProductsPage GoToProductsDetailPage()
        {
            _driver.ScrollToElement(ProductsBtn);
            ProductsBtn.Click();
            ProductsPage productsPage = new ProductsPage(_driver);
            productsPage.waitUntilProductsPageDisplayed();
            return productsPage;
        }

        public ViewCartPage GoToViewCartPage()
        {
            _driver.ScrollToElement(CartBtn);
            CartBtn.Click();
            ViewCartPage viewCartPage = new ViewCartPage(_driver);
            viewCartPage.waitUntilViewCartPageDisplayed();
            return viewCartPage;
        }
        public LogInPage GoToLoginPage()
        {
            _driver.ScrollToElement(SignUpLoginBtn);
            SignUpLoginBtn.Click();
            LogInPage loginPage = new LogInPage(_driver);
            loginPage.waitUntilLoginPageDisplayed();
            return loginPage;
        }

        public LogInPage LogOutUser()
        {
            _driver.ScrollToElement(LogOutBtn);
            LogOutBtn.Click();
            LogInPage loginPage = new LogInPage(_driver);
            loginPage.waitUntilLoginPageDisplayed();
            return loginPage;
        }
        #endregion
    }
}

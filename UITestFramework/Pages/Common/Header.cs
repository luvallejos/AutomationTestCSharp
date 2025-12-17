using OpenQA.Selenium;
using UITestFramework.Utilities;


namespace UITestFramework.Pages.Commons
{
    public class Header
    {
        #region Constants
        private readonly IWebDriver _driver;
        private static readonly By HomeBtn = By.CssSelector("#header i.fa.fa-home");
        private static readonly By ProductsBtn = By.CssSelector("#header i.material-icons.card_travel");
        private static readonly By CartBtn = By.CssSelector("#header i.fa.fa-shopping-cart");
        private static readonly By SignUpLoginBtn = By.CssSelector("#header i.fa.fa-lock");
        private static readonly By LogOutBtn = By.CssSelector("#header i.fa.fa-lock");
        private static readonly By DeleteAccountBtn = By.CssSelector("#header i.fa-trash-o");
        #endregion

        #region Constructors
        public Header(IWebDriver webDriver)
        {
            _driver = webDriver;
        }

        #endregion

        #region Methods
        public HomePage GoToHomePage()
        {
            _driver.ScrollToElement(HomeBtn);
            _driver.Click(HomeBtn);
            HomePage homePage = new HomePage(_driver);
            homePage.WaitUntilHomePageDisplayed();
            return homePage;
        }

        public ProductsPage GoToProductsDetailPage()
        {
            _driver.ScrollToElement(ProductsBtn);
            _driver.Click(ProductsBtn);
            ProductsPage productsPage = new ProductsPage(_driver);
            productsPage.WaitUntilProductsPageDisplayed();
            return productsPage;
        }

        public ViewCartPage GoToViewCartPage()
        {
            _driver.ScrollToElement(CartBtn);
            _driver.Click(CartBtn);
            ViewCartPage viewCartPage = new ViewCartPage(_driver);
            viewCartPage.waitUntilViewCartPageDisplayed();
            return viewCartPage;
        }
        public LogInPage GoToLoginPage()
        {
            _driver.ScrollToElement(SignUpLoginBtn);
            _driver.Click(SignUpLoginBtn);
            LogInPage loginPage = new LogInPage(_driver);
            loginPage.WaitUntilLoginPageIsDisplayed();
            return loginPage;
        }

        public LogInPage LogOutUser()
        {
            _driver.ScrollToElement(LogOutBtn);
            _driver.Click(LogOutBtn);
            LogInPage loginPage = new LogInPage(_driver);
            loginPage.WaitUntilLoginPageIsDisplayed();
            return loginPage;
        }

        public void DeleteAccount() 
        {
            _driver.ScrollToElement(DeleteAccountBtn);
            _driver.Click(DeleteAccountBtn);
        }

        public bool IsUserLogged()
        {
            return _driver.WaitUntilVisible(DeleteAccountBtn) != null ? true : false;
        }
        #endregion
    }
}

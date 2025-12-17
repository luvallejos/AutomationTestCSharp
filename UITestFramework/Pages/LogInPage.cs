using OpenQA.Selenium;
using System;
using System.Threading;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class LogInPage: BasePage
    {
        #region Constants
        private static readonly By LoginPanel = By.CssSelector(".login-form");
        private static readonly By EmailInput = By.CssSelector("input[name='email']");
        private static readonly By PasswordInput = By.CssSelector("input[name='password']");
        private static readonly By LoginBtn = By.CssSelector("button[data-qa='login-button']");
        private static readonly By LoginError = By.XPath(".//p[contains(.,'incorrect')]");
        private static readonly By SignUpPanel = By.CssSelector(".signup-form");
        private static readonly By NameSignUpInput = By.CssSelector("input[data-qa='signup-name']");
        private static readonly By EmailSignUpInput = By.CssSelector("input[data-qa='signup-email']");
        private static readonly By SubmitSignUpBtn = By.CssSelector("input[data-qa='signup-button']");

        #endregion

        #region Constructors
        public LogInPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        #endregion
        #region Methods
        public void WaitUntilLoginPageIsDisplayed()
        {
            _driver.WaitUntilVisible(LoginPanel, "Login Page is not displayed");
        }

        public void Login(string email, string password)
        {
            _driver.Type(EmailInput, email);
            _driver.Type(PasswordInput, password);
            _driver.Click(LoginBtn);
        }

        public bool IsLoginSuccesful()
        {
            if (Header.IsUserLogged())
                return true;
            return false;
        }

        public bool IsLoginErrorDisplayed()
        {
            return _driver.WaitUntilVisible(LoginError) != null ? true : false;
        }

        public SignUpPage GoToSignUpPage(string name, string email)
        {
            _driver.Type(NameSignUpInput, name);
            _driver.Type(EmailSignUpInput, email);
            _driver.Click(SubmitSignUpBtn);
            SignUpPage signUpPage = new SignUpPage(_driver);
            signUpPage.WaitUntilSignUpPageDisplayed();
            return signUpPage;
        }
        #endregion
    }
}

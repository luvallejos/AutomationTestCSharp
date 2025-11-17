using OpenQA.Selenium;
using System.Threading;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class LogInPage: BasePage
    {
        #region Constants
        private const string _loginPanelLocator = ".login-form";
        private const string _emailLoginInputLocator = "input[name='email']";
        private const string _passwordLoginInputLocator = "input[name='password']";
        private const string _submitLoginBtnLocator = "button[data-qa='login-button']";
        private const string _loginErrorMessageXpathLocator = ".//p[contains(text(),'Your email or password is incorrect!')]";
        private const string _signUpPanelLocator = ".signup-form";
        private const string _nameSignUpInputLocator = "input[data-qa='signup-name']";
        private const string _emailSignUpInputLocator = "input[data-qa='signup-email']";
        private const string _submitSignUpBtnLocator = "button[data-qa='signup-button']";
        #endregion

        #region Properties
        public IWebElement LoginPanel => _driver.FindElement(By.CssSelector(_loginPanelLocator));
        public IWebElement LoginEmailInput => LoginPanel.FindElement(By.CssSelector(_emailLoginInputLocator));
        public IWebElement LoginPasswordInput => LoginPanel.FindElement(By.CssSelector(_passwordLoginInputLocator));
        public IWebElement LoginBtn => LoginPanel.FindElement(By.CssSelector(_submitLoginBtnLocator));
        public IWebElement LoginErrorMessage => LoginPanel.FindElement(By.XPath(_loginErrorMessageXpathLocator));
        public IWebElement SignUpPanel => _driver.FindElement(By.CssSelector(_signUpPanelLocator));
        public IWebElement SignUpNameInput => SignUpPanel.FindElement(By.CssSelector(_nameSignUpInputLocator));
        public IWebElement SignUpEmailInput => SignUpPanel.FindElement(By.CssSelector(_emailSignUpInputLocator));
        public IWebElement SignUpBtn => SignUpPanel.FindElement(By.CssSelector(_submitSignUpBtnLocator));

        #endregion

        #region Constructors
        public LogInPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        #endregion
        #region Methods
        public void waitUntilLoginPageDisplayed()
        {
            _driver.WaitUntilDisplayed(LoginPanel, "Login Page is not displayed");
        }

        public void Login(string email, string password)
        {
            LoginEmailInput.Clear();
            LoginEmailInput.SendKeys(email);
            LoginPasswordInput.Clear();
            LoginPasswordInput.SendKeys(password);
            LoginBtn.Click();
            Thread.Sleep(3000); //wait for login process
        }

        public void IsLoginSuccessfull()
        {
            _driver.WaitUntilDisplayed(Header.DeleteAccountBtn, "Login was not succesfull.");
        }

        public void IsLoginNotSuccessfull()
        {

            _driver.WaitUntilDisplayed(LoginErrorMessage, "Login error is not displayed.");
        }

        public SignUpPage GoToSignUpPage(string name, string email)
        {
            SignUpNameInput.Clear();
            SignUpNameInput.SendKeys(name);
            SignUpEmailInput.Clear();
            SignUpEmailInput.SendKeys(email);
            SignUpBtn.Click();
            Thread.Sleep(3000); //wait for Sign Up Page to load 
            SignUpPage signUpPage = new SignUpPage(_driver);
            signUpPage.waitUntilSignUpPageDisplayed();
            return signUpPage;
        }
        #endregion
    }
}

using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;
using UITestFramework.Dto;
using UITestFramework.Pages.Commons;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class SignUpPage
    {
        #region Constants
        private readonly IWebDriver _driver;
        private const string _signUpFormLocator = "form[action='/signup']";
        private const string _genderSignUpRadioLocator = "input#id_gender1";
        private const string _passwordSignUpInputLocator = "input#password";
        private const string _dayBirthDaySelectLocator = "select#days";
        private const string _monthBirthDaySelectLocator = "select#months";
        private const string _yearBirthDaySelectLocator = "select#years";
        private const string _firstNameSignUpInputLocator = "input#first_name";
        private const string _lastNameSignUpInputLocator = "input#last_name";
        private const string _companySignUpInputLocator = "input#company";
        private const string _addressSignUpInputLocator = "input#address1";
        private const string _countrySignUpSelectLocator = "select#country";
        private const string _stateSignUpInputLocator = "input#state";
        private const string _citySignUpInputLocator = "input#city";
        private const string _zipCodeSignUpInputLocator = "input#zipcode";
        private const string _mobileNumberSignUpInputLocator = "input#mobile_number";
        private const string _createAccountBtnLocator = "button[data-qa='create-account']";
        private const string _accountCreatedMessageLocator = "h2[data-qa='account-created']";
        private const string _continueBtnLocator = "a[data-qa='continue-button']";

        private Dictionary<string, string> MonthBirthdayMapping
        {
            get
            {
                var MonthBirthdayMapping = new Dictionary<string, string>(){
                    { "1", "January" },
                    { "2", "February" },
                    { "3", "March" },
                    { "4", "April" },
                    { "5", "May" },
                    { "6", "June" },
                    { "7", "July" },
                    { "8", "August" },
                    { "9", "September" },
                    { "10", "October" },
                    { "11", "November" },
                    { "12", "December" },
                };
                return MonthBirthdayMapping;
            }
        }
        #endregion

        #region Properties
        public Header Header { get; private set; }
        public IWebElement SignUpForm => _driver.FindElement(By.CssSelector(_signUpFormLocator));
        public IWebElement GenderSelector => _driver.FindElement(By.CssSelector(_genderSignUpRadioLocator));
        public IWebElement PasswordInput => _driver.FindElement(By.CssSelector(_passwordSignUpInputLocator));
        public IWebElement DayBirthdaySelector => _driver.FindElement(By.CssSelector(_dayBirthDaySelectLocator));
        public IWebElement MonthBirthdaySelector => _driver.FindElement(By.CssSelector(_monthBirthDaySelectLocator));
        public IWebElement YearBirthdaySelector => _driver.FindElement(By.CssSelector(_yearBirthDaySelectLocator));
        public IWebElement FirstNameInput => _driver.FindElement(By.CssSelector(_firstNameSignUpInputLocator));
        public IWebElement LastNameInput => _driver.FindElement(By.CssSelector(_lastNameSignUpInputLocator));
        public IWebElement CompanyInput => _driver.FindElement(By.CssSelector(_companySignUpInputLocator));
        public IWebElement AddressInput => _driver.FindElement(By.CssSelector(_addressSignUpInputLocator));
        public IWebElement CountrySelector => _driver.FindElement(By.CssSelector(_countrySignUpSelectLocator));
        public IWebElement StateInput => _driver.FindElement(By.CssSelector(_stateSignUpInputLocator));
        public IWebElement CityInput => _driver.FindElement(By.CssSelector(_citySignUpInputLocator));
        public IWebElement ZipCodeInput => _driver.FindElement(By.CssSelector(_zipCodeSignUpInputLocator));
        public IWebElement MobileNumberInput => _driver.FindElement(By.CssSelector(_mobileNumberSignUpInputLocator));
        public IWebElement CreateAccountBtn => _driver.FindElement(By.CssSelector(_createAccountBtnLocator));
        public IWebElement AccountCreatedMessage => _driver.FindElement(By.CssSelector(_accountCreatedMessageLocator));
        public IWebElement ContinueBtn => _driver.FindElement(By.CssSelector(_continueBtnLocator));
        #endregion

        #region Constructor
        public SignUpPage(IWebDriver webDriver)
        {
            _driver = webDriver;
            Header = new Header(_driver);
        }
        #endregion

        #region Methods
        public void waitUntilSignUpPageDisplayed()
        {
            _driver.WaitUntilDisplayed(SignUpForm, "Sign Up Page is not displayed");
        }

        public void SignUpUser(UserData userData) 
        {
            GenderSelector.Click();
            PasswordInput.SendKeys(userData.Password);

            var birthDay = userData.BirthDay.Split('_');
            DayBirthdaySelector.SendKeys(birthDay[2]);
            MonthBirthdaySelector.SendKeys(MonthBirthdayMapping[birthDay[1]]);
            YearBirthdaySelector.SendKeys(birthDay[0]);

            FirstNameInput.SendKeys(userData.FirstName);
            LastNameInput.SendKeys(userData.LastName);
            CompanyInput.SendKeys(userData.Company);
            AddressInput.SendKeys(userData.Address);

            CountrySelector.SendKeys(userData.Country);
            StateInput.SendKeys(userData.State);
            CityInput.SendKeys(userData.City);
            ZipCodeInput.SendKeys(userData.Zipcode);
            MobileNumberInput.SendKeys(userData.MobileNumber);

            CreateAccountBtn.Click();
            Thread.Sleep(3000); //wait for creating account process

            _driver.WaitUntilDisplayed(AccountCreatedMessage, "The account created message is not displayed.");
            ContinueBtn.Click();
        }

        #endregion
    }
}

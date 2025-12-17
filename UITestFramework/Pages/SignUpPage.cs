using OpenQA.Selenium;
using System.Collections.Generic;
using UITestFramework.Dto;
using UITestFramework.Pages.Commons;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class SignUpPage : BasePage
    {
        #region Private Variables
        private static readonly By SignUpForm = By.CssSelector("form[action='/signup']");
        private static readonly By GenderSelector = By.CssSelector("input#id_gender1");
        private static readonly By PasswordInput = By.CssSelector("input#password");
        private static readonly By DayBirthdaySelector = By.CssSelector("select#days");
        private static readonly By MonthBirthdaySelector = By.CssSelector("select#months");
        private static readonly By YearBirthdaySelector = By.CssSelector("select#years");
        private static readonly By FirstNameInput = By.CssSelector("input#first_name");
        private static readonly By LastNameInput = By.CssSelector("input#last_name");
        private static readonly By CompanyInput = By.CssSelector("input#company");
        private static readonly By AddressInput = By.CssSelector("input#address1");
        private static readonly By CountrySelector = By.CssSelector("select#country");
        private static readonly By StateInput = By.CssSelector("input#state");
        private static readonly By CityInput = By.CssSelector("input#city");
        private static readonly By ZipCodeInput = By.CssSelector("input#zipcode");
        private static readonly By MobileNumberInput = By.CssSelector("input#mobile_number");
        private static readonly By CreateAccountBtn = By.CssSelector("button[data-qa='create-account']");
        private static readonly By AccountCreatedMessage = By.CssSelector("h2[data-qa='account-created']");
        private static readonly By ContinueBtn = By.CssSelector("a[data-qa='continue-button']");

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

        #region Constructor
        public SignUpPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        #endregion

        #region Methods
        public void WaitUntilSignUpPageDisplayed()
        {
            _driver.WaitUntilVisible(SignUpForm, "Sign Up Page is not displayed");
        }

        public void SignUpUser(UserData userData) 
        {
            _driver.Click(GenderSelector);
            _driver.Type(PasswordInput, userData.Password);

            var birthDay = userData.BirthDay.Split('_');
            _driver.Select(DayBirthdaySelector, birthDay[2]);
            _driver.Select(MonthBirthdaySelector, MonthBirthdayMapping[birthDay[1]]);
            _driver.Select(YearBirthdaySelector, birthDay[0]);
            _driver.Type(FirstNameInput, userData.FirstName);

            _driver.Type(FirstNameInput,userData.FirstName);
            _driver.Type(LastNameInput, userData.LastName);
            _driver.Type(CompanyInput, userData.Company);
            _driver.Type(AddressInput, userData.Address);

            _driver.Select(CountrySelector, userData.Country);
            _driver.Type(StateInput, userData.State);
            _driver.Type(CityInput, userData.City);
            _driver.Type(ZipCodeInput, userData.Zipcode);
            _driver.Type(MobileNumberInput, userData.MobileNumber);

            _driver.Click(CreateAccountBtn);

            _driver.WaitUntilVisible(AccountCreatedMessage, "The account created message is not displayed.");
            _driver.Click(ContinueBtn);
        }

        #endregion
    }
}

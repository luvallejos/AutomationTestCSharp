using AutomationExercise.Tests;
using AutomationTestCSharp.Utilities;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Threading.Tasks;
using UITestFramework.Pages;
using UITestFramework.Utilities;

namespace AutomationTestCSharp.Tests
{
    [TestFixture]
    internal class SingUpLoginTest: AbstractTest
    {
        #region Variables
        private HomePage _homePage;
        private LogInPage _loginPage;
        private SignUpPage _signUpPage;
        private DeleteUserAccountHelper _deleteUserAccountHelper;
        #endregion

        #region Test Initialize

        [SetUp]
        public void Init()
        {
            driver = Initialize();
            _homePage = new HomePage(driver);
            apiHelper = new APIClientHelper(baseURL);
            _deleteUserAccountHelper = new DeleteUserAccountHelper();
        }

        #endregion

        #region Tests Methods
        [Test]
        [Category("Regression"), Category("SignUpLoginTest")]
        public async Task SignUpAndLoginUserTest()
        {
            await _deleteUserAccountHelper.DeleteUserAccountIfItExists(apiHelper, userData);
            _loginPage = _homePage.Header.GoToLoginPage();
            _signUpPage = _loginPage.GoToSignUpPage(userData.Username, userData.Email);
            _signUpPage.SignUpUser(userData);

            var userExists = await apiHelper.DoesUserAccountExists(userData.Email);
            ClassicAssert.IsTrue(userExists, $"The user was not created for email:{userData.Email}");

            _loginPage = _homePage.Header.LogOutUser();
            _loginPage.Login(userData.Email, userData.Password);
            ClassicAssert.IsTrue(_loginPage.IsLoginSuccesful(), "Login failed.");
        }

        [Test]
        [Category("Regression"), Category("SignUpLoginTest")]
        [TestCase("invalidEmail@gmail.com","invalidPassword")]
        public async Task LoginWithInvalidUserDataTest(string invalidUserEmail, string invalidPassword)
        {
            string fullEmail = $"{DateTime.Now:yyyyMMdd_HHmmss}_{invalidUserEmail}";

            var userExists = await apiHelper.DoesUserAccountExists(fullEmail);
            ClassicAssert.IsFalse(userExists, $"There is a valid user for email:{fullEmail}");

            _loginPage = _homePage.Header.GoToLoginPage();
            _loginPage.Login(invalidUserEmail, invalidPassword);
            ClassicAssert.IsTrue(_loginPage.IsLoginErrorDisplayed(), "Login error message is not displayed.");
        }

        #endregion Tests Methods

        #region Test Clean Up
        [TearDown]
        public void TestCleanUp()
        {
            driver.TakeScreenshotIfTestFailed();
            Cleanup(driver);
        }

        [OneTimeTearDown]
        public async Task TestFixtureCleanUp()
        {
            await _deleteUserAccountHelper.DeleteUserAccountIfItExists(apiHelper, userData);
        }

        #endregion
    }
}

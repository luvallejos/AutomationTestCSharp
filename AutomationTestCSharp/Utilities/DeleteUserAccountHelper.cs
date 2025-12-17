using AutomationExercise.Tests;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Threading;
using System.Threading.Tasks;
using UITestFramework.Dto;
using UITestFramework.Pages;
using UITestFramework.Utilities;

namespace AutomationTestCSharp.Utilities
{
    public class DeleteUserAccountHelper : AbstractTest
    {
        //API /api/deleteAccount is not working, so this the delete account user by UI

        #region Variables
        private HomePage _homePage;
        private LogInPage _logInPage;

        #endregion

        public async Task DeleteUserAccountIfItExists(APIClientHelper apiHelper, UserData userData)
        {
            var userExists = await apiHelper.DoesUserAccountExists(userData.Email);
            if (!userExists)
            {
                TestContext.WriteLine("The user does not exist, we dont need to delete it.");
                return; 
            }

            driver = Initialize();
            _homePage = new HomePage(driver);

            _logInPage = _homePage.Header.GoToLoginPage();
            _logInPage.Login(userData.Email, userData.Password);
            _logInPage.IsLoginSuccesful();
            _homePage.Header.DeleteAccount();
            Thread.Sleep(3000); //wait for account to be deleted 

            userExists = await apiHelper.DoesUserAccountExists(userData.Email);
            ClassicAssert.IsFalse(userExists, "The user was not removed succesfully.");

            driver.Quit();
        }
    }
}

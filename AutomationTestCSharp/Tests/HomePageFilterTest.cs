using AutomationExercise.Tests;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UITestFramework.Pages;
using AutomationTestCSharp.Utilities;

namespace AutomationTestCSharp.Tests
{
    [TestFixture]
    internal class HomePageFilterTest : AbstractTest
    {
        #region Variables
        private HomePage _homePage;

        #endregion

        #region Test Initialize

        [SetUp]
        public void Init()
        {
            driver = Initialize();
            _homePage = new HomePage(driver);
        }

        #endregion

        #region Tests Methods
        [Test]
        [Category("Regression"), Category("SideBarFilterTest")]
        [TestCaseSource(nameof(GetDataRow), new object[] { "SideBarFilterTestSource" })]
        public async Task SideBarFilterByCategoryAndSubcategoryTest(Dictionary<string, string> testData)
        {
            _homePage.SideBar.OpenSubCategory(testData["Category"], testData["Subcategory"]);
            _homePage.FeaturedItems.VerifyTitleIsDisplayed(testData["Category"] + " - " + testData["Subcategory"]);
            _homePage.FeaturedItems.BreadCrumbNavigation.ValidateNavigationBreadcrumb("products," + testData["Category"] + " > " + testData["Subcategory"]);
            await _homePage.FeaturedItems.ValidateProductListDisplayedIsFilteredByCategoryAndSubcategory(testData["Category"], testData["Subcategory"]);
        }

        [Test]
        [Category("Regression"), Category("SideBarFilterTest")]
        [TestCaseSource(nameof(GetDataRow), new object[] { "BrandsFilterTestSource" })]
        public async Task SideBarFilterByBrandTest(Dictionary<string, string> testData)
        {
            _homePage.SideBar.ApplyBrandFilter(testData["Brand"]);
            _homePage.FeaturedItems.VerifyTitleIsDisplayed("Brand - " + testData["Brand"]);
            _homePage.FeaturedItems.BreadCrumbNavigation.ValidateNavigationBreadcrumb("products," + testData["Brand"]);
            await _homePage.FeaturedItems.ValidateProductListDisplayedIsFilteredByBrand(testData["Brand"]);
        }


        #endregion Tests Methods

        #region Test Clean Up
        [TearDown]
        public void TestCleanUp()
        {
            driver.TakeScreenshotIfTestFailed();
            Cleanup(driver);
        }

        #endregion
    }
}

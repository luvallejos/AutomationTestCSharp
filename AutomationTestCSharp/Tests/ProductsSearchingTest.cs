using AutomationExercise.Tests;
using NUnit.Framework;
using System.Collections.Generic;
using UITestFramework.Pages;
using AutomationTestCSharp.Utilities;

namespace AutomationTestCSharp.Tests
{
    [TestFixture]
    internal class ProductsSearchingTest : AbstractTest
    {
        #region Variables
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private ProductDetailsPage _productDetailsPage;
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
        [Category("Regression"), Category("ProductsSearchingTest")]
        [TestCase("Blue")]
        [TestCase("Fancy")]
        [TestCase("Jeans")]
        public void SearchProductsTest(string input)
        {
            _productsPage = _homePage.Header.GoToProductsDetailPage();
            _productsPage.WaitUntilProductsPageDisplayed();
            _productsPage.ValidateResultsForSearchProduct(input);
        }

        [Test]
        [Category("Regression"), Category("ProductsSearchingTest")]
        [TestCaseSource(nameof(GetDataRow), new object[] { "ValidateProductDetailsTestSource" })]
        public void ValidateProductDetailsTest(Dictionary<string, string> testData)
        {
            _productsPage = _homePage.Header.GoToProductsDetailPage();
            _productsPage.WaitUntilProductsPageDisplayed();
            _productDetailsPage = _productsPage.GoToProductDetailsPageByProductName(testData["Name"]);
            _productDetailsPage.ValidateProductInformationDetails(testData);
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

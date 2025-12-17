using AutomationExercise.Tests;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.Linq;
using UITestFramework.Pages;
using AutomationTestCSharp.Utilities;

namespace AutomationTestCSharp.Tests
{
    [TestFixture]
    internal class AddingProductsToCartTest: AbstractTest
    {
        #region Variables
        private HomePage _homePage;
        private ProductsPage _productsPage;
        private ViewCartPage _viewCartPage;
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
        [Category("Regression"), Category("CartTest")]
        [TestCase("Blue Top")]
        [TestCase("Frozen Tops for kids")]
        public void AddOneProductToCartTest(string productName)
        {
            _productsPage = _homePage.Header.GoToProductsDetailPage();
            _productsPage.WaitUntilProductsPageDisplayed();
            _productsPage.AddProductToCartByProductName(productName);
            _viewCartPage = _homePage.AddedToCartModal.GoToViewCartPage();

            ClassicAssert.IsTrue(_viewCartPage.CartTable.IsProductInTableByProductName(productName),
                $"The product '{productName}' was not found in the cart table."
            );
        }
 
        [Test]
        [Category("Regression"), Category("CartTest")]
        [TestCase("Fancy Green Top", "Soft Stretch Jeans", "Grunt Blue Slim Fit Jeans")]
        [TestCase("Men Tshirt", "Summer White top", "Pure Cotton Neon Green Tshirt")]
        public void AddProductsToCartTest(string productName1, string productName2, string productName3)
        {
            _productsPage = _homePage.Header.GoToProductsDetailPage();
            _productsPage.WaitUntilProductsPageDisplayed();

            List<string> productNames = new List<string> { productName1, productName2, productName3 };

            _productsPage.AddListOfProductsToCartByNames(productNames);
            _viewCartPage = _homePage.Header.GoToViewCartPage();
            _viewCartPage.ValidateListOfProductsInCart(productNames);
        }

        [Test]
        [Category("Regression"), Category("CartTest")]
        [TestCase("Fancy Green Top", "Soft Stretch Jeans", "Grunt Blue Slim Fit Jeans")]
        [TestCase("Men Tshirt", "Summer White Top", "Pure Cotton Neon Green Tshirt")]
        public void RemoveProductsFromCartTest(string productName1, string productName2, string productName3)
        {
            _productsPage = _homePage.Header.GoToProductsDetailPage();
            _productsPage.WaitUntilProductsPageDisplayed();

            List<string> productNamesToAdd = new List<string> { productName1, productName2, productName3 };

            _productsPage.AddListOfProductsToCartByNames(productNamesToAdd);
            _viewCartPage = _homePage.Header.GoToViewCartPage();

            List<string> productNamesToRemove = new List<string> { productName1, productName2};
            _viewCartPage.RemoveListOfProductsFromCartByNames(productNamesToRemove);
            _viewCartPage.ValidateListOfProductsInCart(productNamesToAdd.Except(productNamesToRemove).ToList());
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
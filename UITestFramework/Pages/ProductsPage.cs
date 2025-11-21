using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using System.Collections.Generic;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class ProductsPage : BasePage
    {
        #region Constants
        private const string _searchProductsInputLocator = "#search_product";
        private const string _searchProductsSubmitBtnLocator = "#submit_search";
        #endregion

        #region Properties
        public IWebElement SearchProductsTextBox => _driver.FindElement(By.CssSelector(_searchProductsInputLocator));
        public IWebElement SearchProductsSubmitBtn => _driver.FindElement(By.CssSelector(_searchProductsSubmitBtnLocator));

        #endregion

        #region Constructors
        public ProductsPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        #endregion

        #region Methods
        public void waitUntilProductsPageDisplayed()
        {
            _driver.WaitUntilDisplayed(_searchProductsInputLocator, "Product Page is not displayed");
        }

        public void SearchProduct(string input)
        {
            SearchProductsTextBox.SendKeys(input);
            SearchProductsSubmitBtn.Click();
            FeaturedItems.VerifyTitleIsDisplayed("Searched Products");
        }

        public void ValidateResultsForSearchProduct(string input)
        {
            SearchProduct(input);
            var products = FeaturedItems.GetListOfDisplayedProducts();

            products.ForEach(product =>
            {
                ClassicAssert.IsTrue(
                    product.Name.ToLower().Contains(input.ToLower()),
                    $"The product name '{product.Name}' does not contain the search input '{input}'"
                );
            });
        }
        public ProductDetailsPage GoToProductDetailsPageByProductName(string productName)
        {
            SearchProduct(productName);
            IWebElement product = FeaturedItems.GetWebElementProductByName(productName);
            return FeaturedItems.GoToProductDetailsPageForAProduct(product);
        }

        public void AddProductToCartByProductName(string productName)
        {
            SearchProduct(productName);
            IWebElement product = FeaturedItems.GetWebElementProductByName(productName);
            FeaturedItems.AddToCartAProduct(product);
            AddedToCartModal.IsCartModalDisplayed();
        }

        public void AddListOfProductsToCartByNames(List<string> productsNames)
        {
            foreach (var productName in productsNames)
            {
                AddProductToCartByProductName(productName);
                AddedToCartModal.ClickContinueShopping();
                SearchProductsTextBox.Clear();
            }
        }

        #endregion
    }
}

using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using System.Collections.Generic;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class ProductsPage : BasePage
    {
        #region Private Variables
        private static readonly By SearchProductsInput = By.CssSelector("#search_product");
        private static readonly By SearchProductsSubmitBtn = By.CssSelector("#submit_search");
        #endregion

        #region Constructors
        public ProductsPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        #endregion

        #region Methods
        public void WaitUntilProductsPageDisplayed()
        {
            _driver.WaitUntilVisible(SearchProductsInput, "Product Page is not displayed");
        }

        public void SearchProduct(string input)
        {
            _driver.ScrollToElement(SearchProductsInput);
            _driver.Type(SearchProductsInput, input);
            _driver.ScrollToElement(SearchProductsSubmitBtn);
            _driver.Click(SearchProductsSubmitBtn);
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
                _driver.WaitUntilVisible(SearchProductsInput).Clear();
            }
        }

        #endregion
    }
}

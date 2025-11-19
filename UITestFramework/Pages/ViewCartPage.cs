using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using UITestFramework.Dto;
using UITestFramework.Pages.Common;
using UITestFramework.Pages.Commons;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class ViewCartPage
    {
        #region Constants
        private readonly IWebDriver _driver;
        private const string _cartPanelLocator = "#cart_info";
        #endregion

        #region Properties
        public Header Header { get; private set; }
        public BreadCrumbNavigation Navigation { get; private set; }

        public CartTable CartTable { get; private set; }

        public IWebElement CartPanel => _driver.FindElement(By.CssSelector(_cartPanelLocator));

        #endregion

        #region Constructors
        public ViewCartPage(IWebDriver webDriver)
        {
            _driver = webDriver;
            Header = new Header(_driver);
            Navigation = new BreadCrumbNavigation(_driver);
            CartTable = new CartTable(_driver);
        }
        #endregion
        #region Methods
        public void waitUntilViewCartPageDisplayed()
        {
            _driver.WaitUntilDisplayed(CartPanel, "View Cart Page is not displayed");
        }

        public List<Product> GetAllProductsInCart() { 
           
            List<Product> products = new List<Product>();

            CartTable.GetAllRowsOfTable().ForEach(row =>
            {
                var product = new Product
                {
                    Name = row.ProductName,
                    Price = row.ProductPrice,
                    Category = new CategoryInfo
                    {
                        UserType = (ProductUserType)Enum.Parse(typeof(ProductUserType), row.ProductCategoryUserType),
                        Category = (ProductCategory)Enum.Parse(typeof(ProductCategory), row.ProductCategoryCategory),
                    }
                };
                products.Add(product);
            });

            if (products.Count == 0)
            {
                throw new Exception("No products found in the cart.");
            }

            return products;
        }

        public void ValidateListOfProductsInCart(List<string> expectedProductsNames)
        {
            foreach (var productName in expectedProductsNames)
            {
                if(!CartTable.IsProductInTableByProductName(productName))
                {
                    throw new Exception($"Product '{productName}' not found in the cart.");
                }
            }
        }

        public void RemoveListOfProductsFromCartByNames(List<string> productNames)
        {
            foreach (var productName in productNames)
            {
                RemoveProductFromCartByName(productName);
            }
        }

        public void RemoveProductFromCartByName(string productName)
        {
            CartTable.RemoveRowByProductName(productName);
        }

        #endregion
    }
}

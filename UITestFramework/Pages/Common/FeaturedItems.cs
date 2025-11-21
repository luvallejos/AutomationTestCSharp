using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UITestFramework.Dto;
using UITestFramework.Utilities;

namespace UITestFramework.Pages.Commons
{
    public class FeaturedItems
    {
        #region Constants
        private readonly IWebDriver _driver;
        private const string _baseURL = @"https://automationexercise.com";
        private const string _titleLocator = "div.features_items h2";
        private const string _featuredItemsSectionLocator = ".features_items";
        private const string _productListFromFeaturedItemsSectionLocator = "div.single-products";
        private const string _productElementFromFeaturedItemsSectionLocator = "div.product-image-wrapper";
        private const string _addProductToCartBtnLocator = "a[class~='add-to-cart']";
        #endregion

        #region Properties
        public BreadCrumbNavigation BreadCrumbNavigation { get; private set; }
        public IWebElement Title => _driver.FindElement(By.CssSelector(_titleLocator));
        public IWebElement FeaturedItemsSection => _driver.FindElement(By.CssSelector(_featuredItemsSectionLocator));

        #endregion

        #region Constructors
        public FeaturedItems(IWebDriver webDriver)
        {
            _driver = webDriver;
            BreadCrumbNavigation = new BreadCrumbNavigation(webDriver);
        }
        #endregion
        #region Methods
        public void VerifyTitleIsDisplayed(string title)
        {
            _driver.WaitUntilDisplayed(_titleLocator, "Title is not displayed");
            ClassicAssert.IsTrue(Title.Text.ToLower().Contains(title.ToLower()), $"The featured items title is not as expected. Expected: '{title}', Actual: '{Title.Text.Trim()}'");
        }

        public List<Product> GetListOfDisplayedProducts()
        {
            List<IWebElement> products = FeaturedItemsSection.FindElements(By.CssSelector(_productListFromFeaturedItemsSectionLocator)).ToList();

            ClassicAssert.IsNotNull(products, "No products Displayed.");

            var productsList = new List<Product>();
            foreach (var productElement in products)
            {
                productsList.Add(GetProductDataFromFeaturedItemsList(productElement));
            }

            return productsList;
        }

        public IWebElement GetWebElementProductByName(string productName)
        {
            List<IWebElement> products = FeaturedItemsSection.FindElements(By.CssSelector(_productElementFromFeaturedItemsSectionLocator)).ToList();
            IWebElement product = null;

            foreach (var prod in products)
            {
                var match = prod.FindElements(By.CssSelector("p")).FirstOrDefault(p => p.Text.ToLower().Equals(productName.ToLower()));
                if (match != null)
                {
                    product = prod;
                    break;
                }
            }

            ClassicAssert.IsNotNull(product, $"No product get with the name: {productName}.");

            return product;
        }

        public ProductDetailsPage GoToProductDetailsPageForAProduct(IWebElement product)
        {
            _driver.ScrollToElement(product);
            product.FindElement(By.CssSelector(".choose a")).Click();

            ProductDetailsPage productDetailsPage = new ProductDetailsPage(_driver);
            productDetailsPage.waitUntilProductDetailsPageDisplayed();
            return productDetailsPage;
        }

        public void AddToCartAProduct(IWebElement product)
        {
            _driver.ScrollToElement(product);
            product.FindElements(By.CssSelector(_addProductToCartBtnLocator)).First().Click();
        }

        public Product GetProductDataFromFeaturedItemsList(IWebElement productElement)
        {
            var product = new Product();
            product.Name = productElement.FindElements(By.CssSelector("p")).FirstOrDefault().Text;            
            product.Price = Convert.ToInt32(productElement.FindElements(By.CssSelector("h2")).FirstOrDefault().Text.Replace("Rs.", ""));
            return product;
        }

        public async Task ValidateProductListDisplayedIsFilteredByCategoryAndSubcategory(string category, string subcategory)
        {
            List<Product> productListfromUI = GetListOfDisplayedProducts();

            var api = new APIClientHelper(_baseURL);
            List<Product> apiProductsList = await api.GetAllProductsList();
            List<Product> filteredProductsFromAPI = new List<Product>();

            ProductUserType userTypeEnum = Enum.TryParse(category, ignoreCase: true, out ProductUserType userType) ? userType : throw new Exception($"Invalid UserType: '{category}'");
            
            string subcategoryFormatted = subcategory.Replace("&", "").UnescapeTextAndRemoveSpaces();
            ProductCategory subcategoryEnum = Enum.TryParse(subcategoryFormatted, ignoreCase: true, out ProductCategory subcat) ? subcat : throw new Exception($"Invalid Category: '{subcategoryFormatted}'");

            filteredProductsFromAPI = apiProductsList
                .Where(p =>
                    p.Category != null &&
                    p.Category.UserType.HasValue &&
                    p.Category.Category.HasValue &&
                    p.Category.UserType == userTypeEnum &&
                    p.Category.Category == subcategoryEnum)
                .ToList();
            
            foreach (var product in productListfromUI)
            {
                if (!filteredProductsFromAPI.Any(p => p.Name.ToLower() == product.Name.ToLower()))
                {
                    throw new Exception($"Product '{product.Name}' is displayed in UI but not found in API response for Category: '{category}' and Subcategory: '{subcategory}'.");
                }
            }
        }

        public async Task ValidateProductListDisplayedIsFilteredByBrand(string brand)
        {
            List<Product> productListfromUI = GetListOfDisplayedProducts();

            var api = new APIClientHelper(_baseURL);
            List<Product> apiProductsList = await api.GetAllProductsList();
            List<Product> filteredProductsFromAPI = new List<Product>();

            string brandFormatted = brand.Replace("&", "").UnescapeTextAndRemoveSpaces();
            ProductBrand productBrandEnum = Enum.TryParse(brandFormatted, ignoreCase: true, out ProductBrand userType) ? userType : throw new Exception($"Invalid UserType: '{brandFormatted}'");

            filteredProductsFromAPI = apiProductsList
                .Where(p =>
                    p.Brand.HasValue &&
                    p.Brand == productBrandEnum)
                .ToList();

            foreach (var product in productListfromUI)
            {
                if (!filteredProductsFromAPI.Any(p => p.Name.ToLower() == product.Name.ToLower()))
                {
                    throw new Exception($"Product '{product.Name}' is displayed in UI but not found in API response for Brand: '{brand}'.");
                }
            }
        }

        #endregion
    }
}

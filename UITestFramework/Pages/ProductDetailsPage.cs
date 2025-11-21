using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using UITestFramework.Dto;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class ProductDetailsPage : BasePage
    {
        #region Constants
        private const string _productDetailsPanelLocator = ".product-details";
        private const string _productInformationPanelLocator = ".product-information";
        private const string _attributeProductXPathLocator = ".//b[contains(text(), '{0}')]/parent::p";

        #endregion

        #region Properties
        public IWebElement ProductDetailsPanel => _driver.FindElement(By.CssSelector(_productDetailsPanelLocator));
        public IWebElement ProductInformationPanel => _driver.FindElement(By.CssSelector(_productInformationPanelLocator));

        #endregion

        #region Constructors
        public ProductDetailsPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        #endregion

        #region Methods
        public void waitUntilProductDetailsPageDisplayed()
        {
            _driver.WaitUntilDisplayed(_productDetailsPanelLocator, "Products Details Page is not displayed");
        }

        public Product GetProductInformationDetails()
        {
            var product = new Product();
            product.Name = ProductInformationPanel.FindElements(By.CssSelector("h2")).FirstOrDefault().Text;
            product.Price = Convert.ToInt32(ProductInformationPanel.FindElements(By.CssSelector("span span")).FirstOrDefault().Text.Replace("Rs.", ""));
            product.Availability = ProductInformationPanel.FindElements(By.XPath(string.Format(_attributeProductXPathLocator, "Availability"))).FirstOrDefault().Text.Replace("Availability:", "").Trim();
            product.Condition = ProductInformationPanel.FindElements(By.XPath(string.Format(_attributeProductXPathLocator, "Condition"))).FirstOrDefault().Text.Replace("Condition:", "").UnescapeTextAndRemoveSpaces();
            string brandText = ProductInformationPanel.FindElements(By.XPath(string.Format(_attributeProductXPathLocator, "Brand"))).FirstOrDefault().Text.Replace("Brand:", "").Replace("&", "").UnescapeTextAndRemoveSpaces();
            product.Brand = Enum.TryParse(brandText, ignoreCase: true, out ProductBrand brand) ? (ProductBrand?)brand : null;

            string categoryInfoText = ProductInformationPanel.FindElements(By.CssSelector("p")).FirstOrDefault().Text.Replace("Category: ", "").Replace("&","");
            string categoryUserTypetext = categoryInfoText.Split('>').First().UnescapeTextAndRemoveSpaces();
            string categorytext = categoryInfoText.Split('>').Last().UnescapeTextAndRemoveSpaces();

            product.Category = new CategoryInfo
            {
                UserType = Enum.TryParse(categoryUserTypetext, ignoreCase: true, out ProductUserType user) ? (ProductUserType?)user : null,
                Category = Enum.TryParse(categorytext, ignoreCase: true, out ProductCategory cat) ? (ProductCategory?)cat : null,
            };

            return product;
        }

        public void ValidateProductInformationDetails (Dictionary<string, string> expectedProduct)
        {
            Product actualProduct = GetProductInformationDetails();
            ClassicAssert.AreEqual(expectedProduct["Name"].ToLower(), actualProduct.Name.ToLower(), "Product Name is not as expected.");
            ClassicAssert.AreEqual(Convert.ToInt32(expectedProduct["Price"]), actualProduct.Price, "Product Price is not as expected.");
            ClassicAssert.AreEqual(expectedProduct["Condition"].ToLower(), actualProduct.Condition.ToLower(), "Product Condition is not as expected.");
            ClassicAssert.AreEqual(Enum.Parse(typeof(ProductBrand), expectedProduct["Brand"], true), actualProduct.Brand, "Product Brand is not as expected.");
            ClassicAssert.AreEqual(Enum.Parse(typeof(ProductCategory), expectedProduct["Category"], true), actualProduct.Category.Category, "Product Category is not as expected.");
            ClassicAssert.AreEqual(Enum.Parse(typeof(ProductUserType), expectedProduct["UserType"], true), actualProduct.Category.UserType, "Product User Type is not as expected.");
        }

        public void ValidateProductAvailability(Dictionary<string, string> expectedProduct)
        {
            Product actualProduct = GetProductInformationDetails();
            ClassicAssert.AreEqual(expectedProduct["Availability"].ToLower(), actualProduct.Availability.ToLower(), "Product Availability is not as expected.");
        }
        #endregion
    }
}

using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UITestFramework.Dto;

namespace UITestFramework.Pages.Common
{
    public class CartTable
    {
        #region Properties
        private static IWebDriver _driver;
        private const string _tableLocator = "#cart_info_table";
        private const string _tableRowByIndex = "tr[id='product-{0}']";
        private const string _tableRowByProductNameXpathLocator = ".//a[contains(text(),'{0}')]//ancestor::tr";
        private const string _rowProductNameLocator = "td.cart_description a";
        private const string _rowCategoryLocator = "td.cart_description p";
        private const string _rowPriceLocator = "td.cart_price p";
        private const string _rowQuantityLocator = "td.cart_quantity button";
        private const string _rowTotalPriceLocator = "td.cart_total p";
        private const string _rowDeleteBtnLocator = "td.cart_delete a";

        public List<CartTableRow> Rows { get; private set; }
        public IWebElement CartTableElement => _driver.FindElement(By.CssSelector(_tableLocator));
        #endregion

        #region constructors
        public CartTable(IWebDriver webDriver) 
        {
            _driver = webDriver;
            Rows = new List<CartTableRow>();
        }
        #endregion

        #region Methods
        public List<CartTableRow> GetAllRowsOfTable()
        {
            Rows = new List<CartTableRow>();

            List<IWebElement> rowElement = CartTableElement.FindElements(By.CssSelector("tbody tr")).ToList();

            foreach (var row in rowElement)
            {
                Rows.Add(GetRowFromWebElement(row));
            }

            return Rows;
        }
        public CartTableRow GetRowByIndex(int index)
        {
            return GetAllRowsOfTable().Where(r => r.ProductIdIndex.Split('-').Last() == index.ToString()).FirstOrDefault();
        }

        public CartTableRow GetRowByProductName(string productName)
        {
            return GetAllRowsOfTable().Where(r => r.ProductName.ToLower() == productName.ToLower()).FirstOrDefault();
        }

        public void RemoveRowByIndex(int index)
        {
            IWebElement rowElement = CartTableElement.FindElement(By.CssSelector(String.Format(_tableRowByIndex, index + 1)));
            rowElement.FindElement(By.CssSelector(_rowDeleteBtnLocator)).Click();
            Thread.Sleep(3000); //wait for row to be removed

            ClassicAssert.IsNull(GetRowByIndex(index), "Product was not removed from Cart Table.");
        }   

        public void RemoveRowByProductName(string productName)
        {
            IWebElement rowElement = CartTableElement.FindElement(By.XPath(String.Format(_tableRowByProductNameXpathLocator, productName)));
            rowElement.FindElement(By.CssSelector(_rowDeleteBtnLocator)).Click();
            Thread.Sleep(3000); //wait for row to be removed

            ClassicAssert.IsNull(GetRowByProductName(productName), "Product was not removed from Cart Table.");
        }

        public bool IsProductInTableByProductName(string productName)
        {
            try
            {
                var rowElement = GetRowByProductName(productName);
                return rowElement != null;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public CartTableRow GetRowFromWebElement(IWebElement rowElement)
        {
            return new CartTableRow
            {
                ProductIdIndex = rowElement.GetAttribute("id"),
                ProductName = rowElement.FindElement(By.CssSelector(_rowProductNameLocator)).Text,
                ProductCategoryUserType = rowElement.FindElement(By.CssSelector(_rowCategoryLocator)).Text.Split('>').First().Trim(),
                ProductCategoryCategory = rowElement.FindElement(By.CssSelector(_rowCategoryLocator)).Text.Split('>').Last().Trim(),
                ProductPrice = int.Parse(rowElement.FindElement(By.CssSelector(_rowPriceLocator)).Text.Replace("Rs.", "").Trim()),
                ProductQuantity = int.Parse(rowElement.FindElement(By.CssSelector(_rowQuantityLocator)).Text.Trim()),
                ProductTotalPrice = int.Parse(rowElement.FindElement(By.CssSelector(_rowTotalPriceLocator)).Text.Replace("Rs.", "").Trim())
            };
        }


        #endregion
    }
}

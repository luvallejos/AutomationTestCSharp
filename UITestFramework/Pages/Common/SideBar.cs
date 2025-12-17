using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using UITestFramework.Utilities;

namespace UITestFramework.Pages.Commons
{
    public class SideBar
    {
        #region Private Variables
        protected readonly IWebDriver _driver;
        private static readonly By CategoryAccordian = By.CssSelector("#accordian");
        private static readonly By BrandsPanel = By.CssSelector(".brands_products");
        private const string _subcategoriesListAccordianLocator = "a[data-parent='#accordian']"; //get a elements which are clickeable
        private const string _subcategoryAccordianLocator = ".//a[contains(text(),'{0}')]"; //get a elements which are clickeable
        private const string _brandsListLocator = "a"; //get a elements inside brands panel
        #endregion

        #region Constructors
        public SideBar(IWebDriver webDriver)
        {
            _driver = webDriver;
        }

        #endregion

        #region Methods
        public List<string> GetAllCategoriesNames()
        {
            return GetAllCategoriesPanels().Select(x => x.Text.ToLower()).ToList();
        }
        public List<IWebElement> GetAllCategoriesPanels()
        {
            return _driver.WaitUntilVisible(CategoryAccordian).FindElements(By.CssSelector(_subcategoriesListAccordianLocator)).ToList();
        }

        public List<string> GetBrandsNames()
        {
            return GetAllBrandsPanels().Select(x => x.Text.ToLower()).ToList();
        }

        public List<IWebElement> GetAllBrandsPanels()
        {
            return _driver.WaitUntilVisible(BrandsPanel).FindElements(By.CssSelector(_brandsListLocator)).ToList();
        }

        public void ApplyBrandFilter(string filter)
        {
            IWebElement filterToSelect = GetAllBrandsPanels().FirstOrDefault(x => x.Text.ToLower().ToPlainText() == filter.ToLower());
            if (filterToSelect != null)
            {
                //if the panel is collapsed and action is expand, click to expand
                _driver.ScrollToElement(filterToSelect);
                filterToSelect.Click();
                FeaturedItems featuredItemsSection = new FeaturedItems(_driver);
                Assert.That(featuredItemsSection.IsFeaturedItemSectionDisplayed(), Is.True);
            }
            else
            {
                throw new Exception($"Brand '{filter}' not found in the sidebar filter.");
            }
        }

        public IWebElement GetCategoryPanelByName(string category)
        {
            IWebElement panelToSelect = GetAllCategoriesPanels().FirstOrDefault(x => x.Text.ToLower() == category.ToLower());
            if (panelToSelect != null)
            {
                return panelToSelect;
            }
            else
            {
                throw new Exception($"Category '{category}' not found in the sidebar.");
            }
        }

        public IWebElement GetCategoryPanelDivElementByName(string category)
        {
            //get category div element which is not clickeable but it is related to subcategories
            IWebElement panelDivToSelect = _driver.WaitUntilVisible(CategoryAccordian).FindElement(By.Id(category.Capitalize()));
            if (panelDivToSelect != null)
            {
                return panelDivToSelect;
            }
            else
            {
                throw new Exception($"Category '{category}' not found in the sidebar.");
            }
        }

        public void ExpandCollapseCategory(string category, string action)
        {
            IWebElement panelToSelect = GetCategoryPanelByName(category);

            if (panelToSelect != null)
            {
                if (!PanelIsExpanded(category) & (action == "expand"))
                {
                    //if the panel is collapsed and action is expand, click to expand
                    _driver.ScrollToElement(panelToSelect);
                    panelToSelect.Click();

                }
                else
                {
                    if (PanelIsExpanded(category) & (action == "collapse"))
                    {
                        //if the panel is expanded and action is collapse, click to collapse
                        _driver.ScrollToElement(panelToSelect);
                        panelToSelect.Click();
                    }
                }
            }
            else
            {
                throw new Exception($"Category '{category}' not found in the sidebar.");
            }
        }

        public void OpenSubCategory(string category, string subCategory)
        {
            ExpandCollapseCategory(category, "expand");
            IWebElement subcat = GetCategoryPanelDivElementByName(category).FindElements(By.XPath(String.Format(_subcategoryAccordianLocator, subCategory.Capitalize()))).ToList().First();
            
            if (subcat != null)
            {
                _driver.ScrollToElement(subcat);
                subcat.Click();
                FeaturedItems featuredItemsSection = new FeaturedItems(_driver);
                Assert.That(featuredItemsSection.IsFeaturedItemSectionDisplayed(), Is.True);
            }
            else
            {
                throw new Exception($"SubCategory '{subCategory}' not found under Category '{category}' in the sidebar.");
            }
        }

        public bool PanelIsExpanded(string category)
        {
            //return true if the panel is expanded and false if it is collapsed
            return GetCategoryPanelDivElementByName(category).GetAttribute("class").Equals("panel-collapse in");
        }

        #endregion
    }
}

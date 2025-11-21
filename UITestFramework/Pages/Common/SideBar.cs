using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UITestFramework.Utilities;

namespace UITestFramework.Pages.Commons
{
    public class SideBar
    {
        #region Constants
        private readonly IWebDriver _driver;
        private const string _categoryAccordianLocator = "#accordian";
        private const string _subcategoriesListAccordianLocator = "a[data-parent='#accordian']"; //get a elements which are clickeable
        private const string _subcategoryAccordianLocator = ".//a[contains(text(),'{0}')]"; //get a elements which are clickeable
        private const string _brandsPanelLocator = ".brands_products";
        private const string _brandsListLocator = "a"; //get a elements inside brands panel
        #endregion

        #region Properties
        public IWebElement CategoryAccordian => _driver.FindElement(By.CssSelector(_categoryAccordianLocator));
        public IWebElement BrandsPanel => _driver.FindElement(By.CssSelector(_brandsPanelLocator));

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
            return CategoryAccordian.FindElements(By.CssSelector(_subcategoriesListAccordianLocator)).ToList();
        }

        public List<string> GetBrandsNames()
        {
            return GetAllBrandsPanels().Select(x => x.Text.ToLower()).ToList();
        }

        public List<IWebElement> GetAllBrandsPanels()
        {
            return BrandsPanel.FindElements(By.CssSelector(_brandsListLocator)).ToList();
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
                _driver.WaitUntilDisplayed(featuredItemsSection.FeaturedItemsSection, "Failed loading featured Items section.");
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
            IWebElement panelDivToSelect = CategoryAccordian.FindElement(By.Id(category.Capitalize()));
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
                _driver.WaitUntilDisplayed(featuredItemsSection.FeaturedItemsSection, "Failed loading featured Items section.");
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

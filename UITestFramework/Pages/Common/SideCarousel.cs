using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UITestFramework.Pages.Commons
{
    public class SideCarousel
    {
        #region Constants
        private readonly IWebDriver _driver;
        private const string _carouselIndicatorsListLocator = ".carousel-indicators li";


        #endregion

        #region Properties
        public List<IWebElement> CarrouselIndicatorList => _driver.FindElements(By.CssSelector(_carouselIndicatorsListLocator)).ToList();

        #endregion

        #region Constructors
        public SideCarousel(IWebDriver webDriver)
        {
            this._driver = webDriver;
        }

        #endregion

        #region Methods
        public IWebElement GetActiveIndicator()
        {
            IWebElement activeIndic = CarrouselIndicatorList.Where(x => x.GetAttribute("class").Contains("active")).FirstOrDefault();
            if (activeIndic != null)
            {
                return activeIndic;
            }
            else
            {
                throw new Exception("Error getting active indicator in the carousel");
            }
        }

        public void SelectIndicatorByIndex(string index)
        {
            IWebElement indicToSelect = CarrouselIndicatorList.Where(x => x.GetAttribute("data-slide-to").Equals(index)).First();
            if (indicToSelect != null)
            {
                indicToSelect.Click();
                WebDriverWait wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(20));
                wait.Until(driver => indicToSelect.GetAttribute("class").Contains("active"));
                ClassicAssert.IsTrue(indicToSelect.GetAttribute("class").Contains("active"), "Error selecting indicator in the carousel");
            }
            else
            {
                throw new Exception("Error selecting indicator in the carousel");
            }
        }

        #endregion
    }
}


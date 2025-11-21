using OpenQA.Selenium;
using UITestFramework.Pages.Commons;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class HomePage: BasePage
    {
        #region Constants
        private const string _sliderCarrouselLocator = "#slider-carrousel";
        public SideCarousel SideCarousel { get; private set; }

        #endregion

        #region Properties
        public IWebElement SliderCarrousel => _driver.FindElement(By.CssSelector(_sliderCarrouselLocator));

        #endregion

        #region Constructors
        public HomePage(IWebDriver webDriver) : base(webDriver)
        {
            SideCarousel = new SideCarousel(webDriver);
        }

        #endregion

        #region Methods
        public void waitUntilHomePageDisplayed() 
        {
            _driver.WaitUntilDisplayed(_sliderCarrouselLocator, "Home Page is not displayed");
        }

        #endregion

    }
}

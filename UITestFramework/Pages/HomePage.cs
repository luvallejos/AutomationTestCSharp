using OpenQA.Selenium;
using UITestFramework.Pages.Commons;
using UITestFramework.Utilities;

namespace UITestFramework.Pages
{
    public class HomePage: BasePage
    {
        #region Private Variables
        private static readonly By SliderCarrousel = By.CssSelector("#slider-carrousel");
        public SideCarousel SideCarousel { get; private set; }
        #endregion


        #region Constructors
        public HomePage(IWebDriver webDriver) : base(webDriver)
        {
            SideCarousel = new SideCarousel(webDriver);
        }

        #endregion

        #region Methods
        public void WaitUntilHomePageDisplayed() 
        {
            _driver.WaitUntilVisible(SliderCarrousel, "Home Page is not displayed");
        }

        #endregion

    }
}

using OpenQA.Selenium;
using System;

namespace IntegrationAPITests.E2E_tests.Pages
{
    public class BloodBanksPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/blood-banks";

        private IWebElement Table => driver.FindElement(By.Id("Table"));

        public BloodBanksPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public Boolean TableDisplayed()
        {
            return Table.Displayed;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);


    }
}

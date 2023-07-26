using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace IntegrationAPITests.E2E_tests.Pages
{
    public class CheckingBloodQuantityPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/blood-requests";

        private IWebElement SelectBankDropDown => driver.FindElement(By.Id("SelectBankDropDown"));

        private IWebElement SelectBloodTypeDropDown => driver.FindElement(By.Id("SelectBloodTypeDropDown"));

        private IWebElement QuanityInput => driver.FindElement(By.Id("QuanityInput"));

        private IWebElement SubmitButton => driver.FindElement(By.Id("Submit"));

        private IWebElement toast => driver.FindElement(By.CssSelector(".toast-message"));
        public CheckingBloodQuantityPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool SelectBankDropDownDisplayed()
        {
            return SelectBankDropDown.Displayed;
        }

        public bool SelectBloodTypeDropDownDisplayed()
        {
            return SelectBloodTypeDropDown.Displayed;
        }
        public bool QuanityInputDisplayed()
        {
            return QuanityInput.Displayed;
        }

        public void SelectNewLifeBank()
        {

            SelectBankDropDown.Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("New Life")).Click();
        }

        public void SelectBloodType()
        {
            SelectBloodTypeDropDown.Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Aplus")).Click();
        }

        public void TypeInQuantity(string text)
        {
            QuanityInput.Click();
            Thread.Sleep(1000);
            QuanityInput.Clear();
            QuanityInput.SendKeys(text);

        }

        public void SubmitForm()
        {
            SubmitButton.Click();
        }

        public void WaitForToastDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 3));
            wait.Until(condition: ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".toast-message")));
        }
        public bool IsToastVisible()
        {
            return toast.Displayed;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}

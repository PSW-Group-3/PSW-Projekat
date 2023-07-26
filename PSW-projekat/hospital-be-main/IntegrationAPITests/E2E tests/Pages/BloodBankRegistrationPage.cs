using OpenQA.Selenium;

namespace IntegrationAPITests.E2E_tests.Pages
{
    internal class BloodBankRegistrationPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/blood-bank-registration";

        private IWebElement NameField => driver.FindElement(By.Id("Name"));
        private IWebElement EmailField => driver.FindElement(By.Id("Email"));
        private IWebElement AddressField => driver.FindElement(By.Id("ServerAddress"));
        private IWebElement SubmitButton => driver.FindElement(By.Id("Submit"));


        public BloodBankRegistrationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool NameFieldDisplayed()
        {
            return NameField.Displayed;
        }

        public bool EmailFieldDisplayed()
        {
            return EmailField.Displayed;
        }

        public bool AddressFieldDisplayed()
        {
            return AddressField.Displayed;
        }

        public bool SubmitButtonDisplayed()
        {
            return SubmitButton.Displayed;
        }

        public void InsertName(string name)
        {
            NameField.SendKeys(name);
        }

        public void InsertEmail(string email)
        {
            EmailField.SendKeys(email);
        }

        public void InsertAddress(string address)
        {
            AddressField.SendKeys(address);
        }

        public void SubmitForm()
        {
            SubmitButton.Click();
        }
        

        public void Navigate() => driver.Navigate().GoToUrl(URI);

    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace IntegrationAPITests.E2E_tests.Pages
{
    public class ReportSettingsPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/report-settings";

        private IWebElement OptionDeliveryDropdown => driver.FindElement(By.Id("OptionDelivery"));

        private IWebElement DeliveryDateField => driver.FindElement(By.Id("StartDeliveryDate"));

        private IWebElement DeliveryDaysField => driver.FindElement(By.Id("DeliveryDays"));

        private IWebElement DeliveryMonthsField => driver.FindElement(By.Id("DeliveryMonths"));

        private IWebElement DeliveryYearsField => driver.FindElement(By.Id("DeliveryYears"));

        private IWebElement OptionCalculationDropdown => driver.FindElement(By.Id("OptionCalculation"));

        private IWebElement CalculationDaysField => driver.FindElement(By.Id("CalculationDays"));

        private IWebElement CalculationMonthsField => driver.FindElement(By.Id("CalculationMonths"));

        private IWebElement CalculationYearsField => driver.FindElement(By.Id("CalculationYears"));

        private IWebElement SubmitButton => driver.FindElement(By.Id("Submit"));

        private IWebElement Toaster => driver.FindElement(By.CssSelector(".toast-message"));


        public ReportSettingsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitForStart()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(condition: ExpectedConditions.ElementToBeClickable(By.Id("OptionDelivery")));
        }

        public void WaitForComponentToBeClickable(string id)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(condition: ExpectedConditions.ElementToBeClickable(By.Id(id)));
        }

        public bool OptionDeliveryDropdownDisplayed()
        {
            return OptionDeliveryDropdown.Displayed;
        }

        public bool DeliveryDaysFieldDisplayed()
        {
            return DeliveryDaysField.Displayed;
        }

        public bool DeliveryMonthsFieldDisplayed()
        {
            return DeliveryMonthsField.Displayed;
        }

        public bool DeliveryYearsFieldDisplayed()
        {
            return DeliveryYearsField.Displayed;
        }

        public bool OptionCalculationDropdownDisplayed()
        {
            return OptionCalculationDropdown.Displayed;
        }

        public bool CalculationDaysFieldDisplayed()
        {
            return CalculationDaysField.Displayed;
        }

        public bool CalculationMonthsFieldDisplayed()
        {
            return CalculationMonthsField.Displayed;
        }

        public bool CalculationYearsFieldDisplayed()
        {
            return CalculationYearsField.Displayed;
        }

        public void Select6MonthsCalculationPeriod()
        {
            WaitForComponentToBeClickable("OptionCalculation");
            OptionCalculationDropdown.Click();
            driver.FindElement(By.Id("6monthsC")).Click();
        }

        public void SelectMonthCalculationPeriod()
        {
            WaitForComponentToBeClickable("OptionCalculation");
            OptionCalculationDropdown.Click();
            driver.FindElement(By.Id("monthC")).Click();
        }

        public void SelectYearCalculationPeriod()
        {
            WaitForComponentToBeClickable("OptionCalculation");
            OptionCalculationDropdown.Click();
            driver.FindElement(By.Id("yearC")).Click();
        }

        public void SelectCustomCalculationPeriod()
        {
            WaitForComponentToBeClickable("OptionCalculation");
            OptionCalculationDropdown.Click();
            driver.FindElement(By.Id("customC")).Click();
        }

        public void Select6MonthsDeliveryPeriod()
        {
            WaitForComponentToBeClickable("OptionDelivery");
            OptionDeliveryDropdown.Click();
            driver.FindElement(By.Id("6monthsD")).Click();
        }

        public void SelectMonthDeliveryPeriod()
        {
            WaitForComponentToBeClickable("OptionDelivery");
            OptionDeliveryDropdown.Click();
            driver.FindElement(By.Id("monthD")).Click();
        }

        public void SelectYearDeliveryPeriod()
        {
            WaitForComponentToBeClickable("OptionDelivery");
            OptionDeliveryDropdown.Click();
            driver.FindElement(By.Id("yearD")).Click();
        }

        public void SelectCustomDeliveryPeriod()
        {
            WaitForComponentToBeClickable("OptionDelivery");
            OptionDeliveryDropdown.Click();
            driver.FindElement(By.Id("customD")).Click();
        }

        public void InsertCustomDeliveryPeriod(DateTime date, string days, string months, string years)
        {
            WaitForComponentToBeClickable("DeliveryDays");
            DeliveryDaysField.Clear();
            WaitForComponentToBeClickable("DeliveryMonths");
            DeliveryMonthsField.Clear();
            WaitForComponentToBeClickable("DeliveryYears");
            DeliveryYearsField.Clear();
            DeliveryDateField.SendKeys(date.ToShortDateString());
            DeliveryDaysField.SendKeys(days);
            DeliveryMonthsField.SendKeys(months);
            DeliveryYearsField.SendKeys(years);
        }

        private void SetDate(DateTime date)
        {
            DeliveryDateField.Click();
            DeliveryDateField.SendKeys(date.ToShortDateString());
            DeliveryDateField.SendKeys(Keys.Tab);
        }

        public void InsertCustomCalculationPeriod(string days, string months, string years)
        {
            WaitForComponentToBeClickable("CalculationDays");
            CalculationDaysField.Clear();
            WaitForComponentToBeClickable("CalculationMonths");
            CalculationMonthsField.Clear();
            WaitForComponentToBeClickable("CalculationYears");
            CalculationYearsField.Clear();
            CalculationDaysField.SendKeys(days);
            CalculationMonthsField.SendKeys(months);
            CalculationYearsField.SendKeys(years);
        }

        public void SubmitForm()
        {
            SubmitButton.Click();
        }

        public void WaitForToastDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 2));
            wait.Until(condition: ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".toast-message")));
        }

        public void ToastSuccessfullDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(condition: ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".toast-success")));
        }

        public void ToastErrorDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            wait.Until(condition: ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".toast-error")));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace HospitalTests.E2E_tests.Pages
{
    public class ReschedulingAppointmentPage
    {

        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/appointments/1/update";
        private IWebElement DateTimeField => driver.FindElement(By.Id("dateTime"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("submitButton"));
        private IWebElement toast => driver.FindElement(By.CssSelector(".toast-message"));

        public const string InvalidPatientMessage = "Patient should be defined!";
        public const string InvalidDateTimeMessage = "DateTime should be defined!";

        public ReschedulingAppointmentPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool DateTimeFieldDisplayedOnScreen()
        {
            return DateTimeField.Displayed;
        }

        public string GetDialogMessage()
        {
            return driver.SwitchTo().Alert().Text;
        }

        public void DateTimeFieldDisplayed(DateTime date)
        {
            DateTimeField.SendKeys(date.ToString());
        }

        public bool SubmitButtonElementDisplayed()
        {
            return SubmitButtonElement.Displayed;
        }

        public void SubmitForm()
        {
            SubmitButtonElement.Click();
        }

        public void WaitForAlertDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public void WaitForToastDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(condition: ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".toast-message")));
        }
        public bool IsToastVisible()
        {
            return toast.Displayed;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);

    }
}

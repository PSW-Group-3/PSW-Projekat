using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace HospitalTests.E2E_tests.Pages
{
    public class SchedulingAppointmentPage
    {
        //svakom page klasom se opisuje jedna web stranica
        //unutar page klase se nalaze svi atributi i metode koje se koriste pri interakciji sa odredjenom stranicom

        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/appointments/add";
        private IWebElement PatientSelectButton => driver.FindElement(By.Id("patientSelect"));
        private IWebElement DateTimeField => driver.FindElement(By.Id("dateTime"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("submitButton"));
        private IWebElement toast => driver.FindElement(By.CssSelector(".toast-message"));

        public string Title => driver.Title;

        public const string InvalidPatientMessage = "Patient should be defined!";
        public const string InvalidDateTimeMessage = "DateTime should be defined!";

        public SchedulingAppointmentPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return PatientSelectButtonDisplayedOnScreen() && DateTimeFieldDisplayedOnScreen() && SubmitButtonElementDisplayed();
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public bool PatientSelectButtonDisplayedOnScreen()
        {
            return PatientSelectButton.Displayed;
        }

        public bool DateTimeFieldDisplayedOnScreen()
        {
            return DateTimeField.Displayed;
        }

        public void PatientSelectButtonDisplayed()
        {
            PatientSelectButton.Click();
            driver.FindElement(By.Id("mikica")).Click();
        }

        public void DateTimeFieldDisplayed(DateTime date)
        {
            DateTimeField.SendKeys(date.ToString());
        }

        public string GetSelectedPatient()
        {
            return PatientSelectButton.Text;
        }

        public string GetDateTimeField()
        {
            return DateTimeField.Text;
        }

        public DateTime GetDateTime(DateTime dateTimeField)
        {
            return dateTimeField;
        }

        public string GetDialogMessage()
        {
            return driver.SwitchTo().Alert().Text;
        }
        public void ResolveAlertDialog()
        {
            driver.SwitchTo().Alert().Accept();
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
            wait.Until(ExpectedConditions.AlertIsPresent());
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(AppoinmtentsPage.URI));
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

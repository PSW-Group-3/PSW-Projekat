using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTests.e2e_T10.Pages
{
    public class CancelAppointmentPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/appointments";
        public static bool Err { get; set; } = true;

        private IWebElement AppointmentsTable => driver.FindElement(By.Id("appointmentsTable"));
        private IWebElement CancelButton => driver.FindElement(By.Id("1"));

        public IWebElement Status => driver.FindElement(By.Id("11"));

        public CancelAppointmentPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool AppointmentsTableDisplayed()
        {
            return AppointmentsTable.Displayed;
        }

        public bool CancelButtonDisplayed()
        {
            return CancelButton.Displayed;
        }

        public void SubmitCancel()
        {
            CancelButton.Click();
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return AppointmentsTableDisplayed() && CancelButtonDisplayed();
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

        public void EnsureCanceled()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return Status.Text == "Cancelled";
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

        public void WaitForToastDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 3));
            wait.Until(condition: ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".toast-message")));
        }
    }
}

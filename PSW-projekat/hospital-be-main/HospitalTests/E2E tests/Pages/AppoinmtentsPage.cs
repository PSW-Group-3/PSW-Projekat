using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HospitalTests.E2E_tests.Pages
{
    public class AppoinmtentsPage
    {

        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/appointments";
        private IWebElement Table => driver.FindElement(By.Id("appoitmentsTable"));
        private ReadOnlyCollection<IWebElement> Rows => driver.FindElements(By.XPath("//table[@id='appoitmentsTable']/tbody/tr"));
        private IWebElement LastRowDateAndTime => driver.FindElement(By.XPath("//table[@id='appoitmentsTable']/tbody/tr[last()]/td[1]"));
        private IWebElement LastRowPatientName => driver.FindElement(By.XPath("//table[@id='appoitmentsTable']/tbody/tr[last()]/td[2]"));
        private IWebElement LastRowPatientSurname => driver.FindElement(By.XPath("//table[@id='appoitmentsTable']/tbody/tr[last()]/td[3]"));
        private IWebElement Link => driver.FindElement(By.Id("createAppoitment"));
        public string Title => driver.Title;
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return Rows.Count > 0;
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

        public bool ErrorDivDisplayed()
        {
            try
            {
                return driver.FindElement(By.Id("ErrorDiv")).Displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool LinkDisplayed()
        {
            return Link.Displayed;
        }
        public void ClickLink()
        {
            Link.Click();
        }

        public AppoinmtentsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        
        public int ProductsCount()
        {
            return Rows.Count;
        }

        public string GetLastRowDateAndTime()
        {
            return LastRowDateAndTime.Text;
        }

        public string GetLastRowPatientName()
        {
            return LastRowPatientName.Text;
        }

        public string GetLastRowPatientSurname()
        {
            return LastRowPatientSurname.Text;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }

}

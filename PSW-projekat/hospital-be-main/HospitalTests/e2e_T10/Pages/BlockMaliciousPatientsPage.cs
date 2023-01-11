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
    public class BlockMaliciousPatientsPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/malicious-patients";
        public static bool Err { get; set; } = true;

        private IWebElement PatientsTable => driver.FindElement(By.Id("patientsTable"));
        private IWebElement BlockButton => driver.FindElement(By.Id("9Block"));
        private IWebElement UnblockButton => driver.FindElement(By.Id("10Unblock"));
        private IWebElement NewBlockButton => driver.FindElement(By.Id("9Unblock"));
        private IWebElement NewUnblockButton => driver.FindElement(By.Id("10Block"));

        public IWebElement isBlockedFirst => driver.FindElement(By.XPath("//table[@id='patientsTable']/tbody/tr[1]/td[3]"));
        public IWebElement isBlockedLast => driver.FindElement(By.XPath("//table[@id='patientsTable']/tbody/tr[last()]/td[3]"));

        public BlockMaliciousPatientsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool PatientsTableDisplayed()
        {
            return PatientsTable.Displayed;
        }

        public void SubmitBlock()
        {
            BlockButton.Click();
        }

        public void SubmitUnblock()
        {
            UnblockButton.Click();
        }

        public Boolean BlockButtonHasDisappeared()
        {
            return NewBlockButton.Enabled;
        }

        public Boolean UnblockButtonHasDisappeared()
        {
            return NewUnblockButton.Enabled;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return PatientsTableDisplayed();
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

        public void EnsureBlockButtonHasDisappeared()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return BlockButtonHasDisappeared();
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

        public void EnsureUnblockButtonHasDisappeared()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return UnblockButtonHasDisappeared();
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
    }
}

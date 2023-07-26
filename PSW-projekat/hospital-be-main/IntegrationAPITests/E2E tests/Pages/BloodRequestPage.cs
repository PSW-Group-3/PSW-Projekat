using OpenQA.Selenium;
using System.Threading;

namespace IntegrationAPITests.E2E_tests.Pages
{
    public class BloodRequestPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/doctor-blood-requests";
        private IWebElement req1 => driver.FindElement(By.Id("object-0"));
        private IWebElement req2 => driver.FindElement(By.Id("object-1"));
        private IWebElement req3 => driver.FindElement(By.Id("object-2"));

        public BloodRequestPage(IWebDriver driver)
        {
            this.driver = driver;
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
        public void SelectReq1()
        {
            req1.Click();
            Thread.Sleep(1000);
        }
        public void SelectReq2()
        {
            req2.Click();
            Thread.Sleep(1000);
        }
        public void SelectReq3()
        {
            req3.Click();
            Thread.Sleep(1000);
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);

    }
}

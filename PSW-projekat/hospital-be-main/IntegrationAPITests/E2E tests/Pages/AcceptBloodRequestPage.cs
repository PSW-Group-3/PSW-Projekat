using OpenQA.Selenium;
using System.Threading;

namespace IntegrationAPITests.E2E_tests.Pages
{
    public class AcceptBloodRequestPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/doctor-blood-request/1";

        private IWebElement AcceptRequestButton => driver.FindElement(By.Id("accept-request-button"));
        private IWebElement SendRequestButton => driver.FindElement(By.Id("send-request-button"));
        private IWebElement DeclineRequestButton => driver.FindElement(By.Id("decline-request-button"));
        private IWebElement ReturnBackRequestButton => driver.FindElement(By.Id("return-back-request-button"));
        private IWebElement SubmitReturnButton => driver.FindElement(By.Id("submit-return-button"));
        private IWebElement SelectBloodBank => driver.FindElement(By.Id("select-blood-bank"));
        private IWebElement FirstBloodBank => driver.FindElement(By.Id("bank-0"));
        private IWebElement ReasonReturnBackText => driver.FindElement(By.Id("reason-return-back-text"));
        public AcceptBloodRequestPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool SelectBloodBankDisplayed()
        {
            return SelectBloodBank.Displayed;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);
        public void Decline()
        {
            DeclineRequestButton.Click();
        }
        public void Return_back()
        {
            ReturnBackRequestButton.Click();
            ReasonReturnBackText.SendKeys("Test123");
            //Thread.Sleep(500);
            //driver.FindElement(By.Id("submit-return-button")).Click();
            SubmitReturnButton.Click();

        }
        public void Accept()
        {
            AcceptRequestButton.Click();
            Thread.Sleep(1000);
            SelectBloodBank.Click();
            FirstBloodBank.Click();
            Thread.Sleep(5000);
            SendRequestButton.Click();
        }
    }
}

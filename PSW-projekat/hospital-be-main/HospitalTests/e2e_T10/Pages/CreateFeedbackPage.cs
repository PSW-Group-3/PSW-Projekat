using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace HospitalTests.e2e_T10.Pages
{
    public class CreateFeedbackPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/feedbacks/add";
        private IWebElement DescriptionElement => driver.FindElement(By.Id("description"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("submitButton"));

        public string Title => driver.Title;
        public const string DataMissingMessage = "Description is missing";
        public const string FeedBackCreatedMessage = "Feedback created";

        public CreateFeedbackPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool DescriptionElementDisplayed()
        {
            return DescriptionElement.Displayed;
        }

        public bool SubmitButtonElementDisplayed()
        {
            return SubmitButtonElement.Displayed;
        }

        public void InsertDescription(string des)
        {
            DescriptionElement.SendKeys(des);
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

        public string GetDialogMessage()
        {
            return driver.SwitchTo().Alert().Text;
        }

        public void ResolveAlertDialog()
        {
            driver.SwitchTo().Alert().Accept();
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/"));
        }
    }
}

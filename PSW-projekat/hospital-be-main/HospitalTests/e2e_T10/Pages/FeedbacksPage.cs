using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace HospitalTests.e2e_T10.Pages
{
    public class FeedbacksPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/feedbacks";

        private IWebElement FeedbacksTable => driver.FindElement(By.Id("feedbacksTable"));

        private IWebElement ApproveButton => driver.FindElement(By.Id("approveButton4"));
        private IWebElement RejectButton => driver.FindElement(By.Id("rejectButton4"));

        public FeedbacksPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool FeedbacksTableElementDisplayed()
        {
            return FeedbacksTable.Displayed;
        }

        public bool ApproveButtonElementDisplayed()
        {
            return ApproveButton.Displayed;
        }

        public bool RejectButtonElementDisplayed()
        {
            return RejectButton.Displayed;
        }

        public void ApproveFeedback()
        {
            ApproveButton.Click();
        }

        public void RejectFeedback()
        {
            RejectButton.Click();
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return FeedbacksTableElementDisplayed();
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

        public void EnsureApproveButtonHasDisappeared()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return RejectButtonElementDisplayed();
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
        public void EnsureRejectButtonHasDisappeared()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return ApproveButtonElementDisplayed();
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

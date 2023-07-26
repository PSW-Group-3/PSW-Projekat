using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using Xunit;

namespace HospitalTests.e2e_T10
{
    public class ApproveFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.FeedbacksPage feedbacksPage;

        public ApproveFeedbackTests()
        {
            // options for launching Google Chrome
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications

            driver = new ChromeDriver(options);

            loginPage = new Pages.LoginPage(driver);
            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            loginPage.InsertUsername("pera");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.EnsureLoggedIn();
            loginPage.ErrorDivDisplayed().ShouldBe(false);
            feedbacksPage = new Pages.FeedbacksPage(driver);
            feedbacksPage.Navigate();
            feedbacksPage.EnsurePageIsDisplayed();

        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Test_approve_feedback()
        {
            feedbacksPage.ApproveFeedback();

            feedbacksPage.EnsureApproveButtonHasDisappeared();      

            bool approveButtonDissapeared = feedbacksPage.RejectButtonElementDisplayed();

            Assert.True(approveButtonDissapeared);      
        }

        [Fact]
        public void Test_reject_feedback()
        {
            feedbacksPage.RejectFeedback();

            feedbacksPage.EnsureRejectButtonHasDisappeared();

            bool rejectButtonDisappeared = feedbacksPage.ApproveButtonElementDisplayed();

            Assert.True(rejectButtonDisappeared);
        }
    }
}

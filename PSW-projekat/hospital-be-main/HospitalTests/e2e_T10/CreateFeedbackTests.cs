using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using System;
using System.Threading;
using Xunit;

namespace HospitalTests.e2e_T10
{
    public class CreateFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.CreateFeedbackPage feedbackPage;

        public CreateFeedbackTests()
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
            loginPage.InsertUsername("nevena");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            Thread.Sleep(3000);
            loginPage.ErrorDivDisplayed().ShouldBe(false);
            feedbackPage = new Pages.CreateFeedbackPage(driver);
            feedbackPage.Navigate();

            //Assert.True(feedbackPage.DescriptionElementDisplayed());
            //Assert.True(feedbackPage.SubmitButtonElementDisplayed());

        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void TestValidFeedbackDescription()
        {
            feedbackPage.InsertDescription("Appointment was excellent");          // insert all values except name
            feedbackPage.SubmitForm();

            feedbackPage.WaitForAlertDialog();         // wait for alert dialog
            Assert.Equal(feedbackPage.GetDialogMessage(), Pages.CreateFeedbackPage.FeedBackCreatedMessage);
            feedbackPage.ResolveAlertDialog();         // accept dialog
            Assert.Equal(driver.Url, Pages.CreateFeedbackPage.URI);      // check if same url - page not submitted
        }

        [Fact]
        public void TestMissingDescription()
        {
            //feedbackPage.InsertDescription("Appointment was excellent");          // insert all values except name
            feedbackPage.SubmitForm();

            feedbackPage.WaitForAlertDialog();         // wait for alert dialog
            Assert.Equal(feedbackPage.GetDialogMessage(), Pages.CreateFeedbackPage.DataMissingMessage);     // check if alert message expected
            feedbackPage.ResolveAlertDialog();         // accept dialog
            Assert.Equal(driver.Url, Pages.CreateFeedbackPage.URI);      // check if same url - page not submitted
        }
    }
}

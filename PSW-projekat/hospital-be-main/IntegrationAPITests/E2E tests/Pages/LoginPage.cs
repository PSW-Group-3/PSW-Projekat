using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAPITests.E2E_tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/login";
        public static bool Err {get; set;} = true;

        private IWebElement UsernameField => driver.FindElement(By.Id("username"));

        private IWebElement PasswordField => driver.FindElement(By.Id("password"));

        private IWebElement SubmitButton => driver.FindElement(By.Id("submit"));

        public LoginPage(IWebDriver driver)
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

        public bool UsernameFieldDisplayed()
        {
            Err = UsernameField.Displayed;
            return UsernameField.Displayed;
        }

        public bool PasswordFieldDisplayed()
        {
            return PasswordField.Displayed;
        }

        public bool SubmitButtonDisplayed()
        {
            return SubmitButton.Displayed;
        }

        public void InsertUsername(string username)
        {
            UsernameField.SendKeys(username);
        }

        public void InsertPassword(string password)
        {
            PasswordField.SendKeys(password);
        }

        public void SubmitForm()
        {
            SubmitButton.Click();
        }

        public void WaitForToastDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(condition: ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("ErrorDiv")));
        }

        public void WaitForLoginChangePage()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(condition: ExpectedConditions.UrlContains("home"));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return UsernameFieldDisplayed() && PasswordFieldDisplayed() && SubmitButtonDisplayed();
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

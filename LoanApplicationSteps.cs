using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using Xunit;

namespace DemoWebApp.Tests
{
    [Binding]
    public class LoanApplicationSteps
    {
        private IWebDriver _driver;

        [Given(@"I am on the loan application screen")]
        public void GivenIAmOnTheLoanApplicationScreen()
        {
            _driver = new ChromeDriver();

            _driver.Manage().Window.Maximize();

            _driver.Navigate().GoToUrl("http://localhost:40077/Home/StartLoanApplication");
        }
        
        [Given(@"I enter a first name of (.*)")]
        public void GivenIEnterAFirstNameOf(string firstName)
        {
            IWebElement firstNameInput = _driver.FindElement(By.Id("FirstName"));
            firstNameInput.SendKeys(firstName);
        }
        
        [Given(@"I enter a second name of (.*)")]
        public void GivenIEnterASecondNameOf(string secondName)
        {
            _driver.FindElement(By.Id("LastName")).SendKeys(secondName);
        }
        
        [Given(@"I select that I have an existing loan account")]
        public void GivenISelectThatIHaveAnExistingLoanAccount()
        {
            _driver.FindElement(By.Id("Loan")).Click();
        }
        
        [Given(@"I confirm my acceptance of the terms and conditions")]
        public void GivenIConfirmMyAcceptanceOfTheTermsAndConditions()
        {
            _driver.FindElement(By.Name("TermsAcceptance")).Click();
        }
        
        [When(@"I submit my application")]
        public void WhenISubmitMyApplication()
        {
            _driver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
        }
        
        [Then(@"I should see the application complete confirmation for Sarah")]
        public void ThenIShouldSeeTheApplicationCompleteConfirmationForSarah()
        {
            IWebElement confirmationNameSpan = _driver.FindElement(By.Id("firstName"));

            string confirmationName = confirmationNameSpan.Text;

            Assert.Equal("Sarah", confirmationName);
        }

        [Then(@"I should see an error message telling me I must the accept the terms and conditions")]
        public void ThenIShouldSeeAnErrorMessageTellingMeIMustTheAcceptTheTermsAndConditions()
        {
            IWebElement errorElement =
                _driver.FindElement(By.CssSelector("div.validation-summary-errors ul li"));

            Assert.Equal("You must accept the terms and conditions", errorElement.Text);
        }



        [AfterScenario]
        public void DisposeWebDriver()
        {
            _driver.Dispose();
        }
    }
}

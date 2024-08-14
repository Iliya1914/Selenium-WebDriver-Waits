using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseSeleniumWaits
{
    [TestFixture]
    public class WorkingWithAlerts
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandleBasicJavaScriptAlerts()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Alert')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert text is not as expected");

            alert.Accept();

            IWebElement alertResult = driver.FindElement(By.Id("result"));

            Assert.That(alertResult.Text, Is.EqualTo("You successfully clicked an alert"), "Result message is not as expected");
        }

        [Test]

        public void HandleJavaScriptConfirmAlerts()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected");

            alert.Accept();

            IWebElement alertResult = driver.FindElement(By.Id("result"));

            Assert.That(alertResult.Text, Is.EqualTo("You clicked: Ok"), "Result message is not as expected");

            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected");

            alert.Dismiss();

            Assert.That(alertResult.Text, Is.EqualTo("You clicked: Cancel"), "Result message is not as expected");
        }

        [Test]
        public void HandleJavascriptPromptAlerts() 
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Prompt')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text is not as expected");

            alert.SendKeys("Levski");
            alert.Accept();

            IWebElement alertResult = driver.FindElement(By.Id("result"));

            Assert.That(alertResult.Text, Is.EqualTo("You entered: Levski"), "Result message is not as expected");
        }
    }
}

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace ExerciseSeleniumWaits
{
    [TestFixture]
    public class WorkingWithIFrames
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandleIFramesByIndex()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            IWebElement dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='dropbtn']")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']//a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void HandleIFramesById()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("result")));

            IWebElement dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='dropbtn']")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']//a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void HandleIFramesByWebElement()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

            var iframeElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("result")));

            driver.SwitchTo().Frame(iframeElement);

            IWebElement dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='dropbtn']")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='dropdown-content']//a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected");
            }

            driver.SwitchTo().DefaultContent();
        }
    }
}
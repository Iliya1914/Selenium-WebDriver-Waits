using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ExerciseSeleniumWaits
{
    [TestFixture]
    public class SearchProductWithExplicitWait
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void SearchForKeyboardShouldAddToCart()
        {
            driver.FindElement(By.XPath("//input[@type='text'][@name='keywords']")).SendKeys("keyboard");

            driver.FindElement(By.XPath("//input[@type='image']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
                IWebElement buyNowButton = wait.Until(e => e.FindElement(By.XPath("//span[contains(text(),'Buy Now')]")));

                buyNowButton.Click();

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                IWebElement checkoutButton = driver.FindElement(By.XPath("//a[@id='tdb5']//span[@class='ui-button-text']"));
                Assert.That(checkoutButton, Is.Not.Null, "Element is present on the page.");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Element is not present on the page.");
            }
        }

        [Test]
        public void SearchForJunkShouldThrowNoSuchElementException()
        {
            driver.FindElement(By.XPath("//input[@type='text'][@name='keywords']")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@type='image']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
                IWebElement buyNowButton = wait.Until(e => e.FindElement(By.XPath("//span[contains(text(),'Buy Now')]")));

                buyNowButton.Click();
                Assert.Fail("Buy Now button was found for a non-existing product");
            }
            catch (WebDriverTimeoutException)
            {

                Assert.Pass("Exception TimeoutException was thrown");
            }
            finally
            { 
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
    }
}

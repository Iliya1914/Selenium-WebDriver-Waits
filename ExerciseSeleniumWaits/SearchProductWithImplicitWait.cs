using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ExerciseSeleniumWaits
{
    [TestFixture]
    public class SearchProductWithImplicitWaitts
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

            try
            {
                driver.FindElement(By.XPath("//span[contains(text(),'Buy Now')]")).Click();

                IWebElement checkoutButton = driver.FindElement(By.XPath("//a[@id='tdb5']//span[@class='ui-button-text']"));
                Assert.IsNotNull(checkoutButton, "Element is present on the page.");
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

            try
            {
                driver.FindElement(By.XPath("//span[contains(text(),'Buy Now')]")).Click();
            }
            catch (NoSuchElementException)
            {
                Assert.Pass("Exception NoSuchElementException was thrown");
            }
        }
    }
}
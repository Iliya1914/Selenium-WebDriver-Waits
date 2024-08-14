using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ExerciseSeleniumWaits
{

    [TestFixture]
    public class WorkingWithWindows
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandlingMultipleWindows()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> handles = driver.WindowHandles;

            Assert.That(handles.Count, Is.EqualTo(2), "There should be two windows open");

            driver.SwitchTo().Window(handles[1]);

            IWebElement newTabMessage = driver.FindElement(By.XPath("//div[@class='example']//h3"));

            Assert.That(newTabMessage.Text, Is.EqualTo("New Window"), "The content of the new window is not as expected");

            string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, $"Window handle for the new window: {driver.CurrentWindowHandle}\n\n");
            File.AppendAllText(path, $"The page content: {driver.PageSource}\n\n");

            driver.Close();
            driver.SwitchTo().Window(handles[0]);

            IWebElement originTabMessage = driver.FindElement(By.XPath("//div[@class='example']//h3"));

            Assert.That(originTabMessage.Text, Is.EqualTo("Opening a new window"), "The content of the origin window is not as expected");

            File.AppendAllText(path, $"Window handle for the original window: {driver.CurrentWindowHandle}\n\n");
            File.AppendAllText(path, $"The page content: {driver.PageSource}\n\n");
        }

        [Test]
        public void HandlingNoSuchWindowException()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> handles = driver.WindowHandles;

            Assert.That(handles.Count, Is.EqualTo(2), "There should be two windows open");

            driver.SwitchTo().Window(handles[1]);

            driver.Close();

            try
            {
                driver.SwitchTo().Window(handles[1]);
            }
            catch (NoSuchWindowException)
            {
                Assert.Pass("NoSuchWindowException wa correctly handled");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An unexpected exception was thrown: {ex.Message}");
            }
            finally
            {
                driver.SwitchTo().Window(handles[0]);
            }
        }
    }
}

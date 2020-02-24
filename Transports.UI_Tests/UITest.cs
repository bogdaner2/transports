using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Transports.UI_Tests
{
    public class UITest
    {
        private IWebDriver driver;

        [SetUp]
        public void Init()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost:3000/";
        }

        [Test]
        public void TestNewDriver()
        {
            Thread.Sleep(2000);
            var initCount = driver.FindElements(By.ClassName("item")).Count;

            CreateNewDriver();

            var resCount = driver.FindElements(By.ClassName("item")).Count;

            Assert.AreNotEqual(initCount, resCount);
        }

        [Test]
        public void TestUpdate()
        {
            Thread.Sleep(2000);
            var items = driver.FindElements(By.ClassName("item"));
            var item = items.Last();
            var controls = item.FindElement(By.ClassName("controls"));
            var updateBtn = controls.FindElement(By.ClassName("custom-button-primary"));
            updateBtn.Click();

            driver.FindElement(By.Id("name")).SendKeys("Update");

            driver.FindElement(By.Id("submit")).Click();

            Thread.Sleep(1000);

            var value = item.FindElements(By.TagName("div"))
                .Skip(1).Take(1).First()
                .FindElements(By.TagName("span")).Skip(1).Take(1).First().Text;

            Assert.IsTrue(value.Contains("Update"));
        }

        [Test]
        public void TestClear()
        {
            Thread.Sleep(2000);
            var items = driver.FindElements(By.ClassName("item"));
            var item = items.Last();
            var controls = item.FindElement(By.ClassName("controls"));
            var updateBtn = controls.FindElement(By.ClassName("custom-button-primary"));
            updateBtn.Click();
            Thread.Sleep(2000);
            var clearButton = driver.FindElement(By.Id("clear"));

            Assert.AreEqual(clearButton.Displayed, true);

            var name = driver.FindElement(By.Id("name")).GetAttribute("value");

            clearButton.Click();

            Thread.Sleep(500);

            var clearedName = driver.FindElement(By.Id("name")).GetAttribute("value"); 
            
            Assert.AreNotEqual(name, clearedName);

            try
            {
                var clearBtn = driver.FindElement(By.Id("clear"));
            }
            catch (Exception e)
            {
                Assert.IsFalse(false);
            }
        }

        [Test]
        public void TestDelete()
        {
            Thread.Sleep(2000);
            var items = driver.FindElements(By.ClassName("item"));
            var item = items.Last();
            var controls = item.FindElement(By.ClassName("controls"));
            var deleteBtn = controls.FindElement(By.ClassName("custom-button-accent"));
            deleteBtn.Click();
            Thread.Sleep(1000);

            Assert.AreNotEqual(items.Count, driver.FindElements(By.ClassName("item")));
        }

        [Test]
        public void CheckDriversList()
        {
            var initCount = driver.FindElements(By.ClassName("item")).Count;

            Thread.Sleep(2000);

            var resCount = driver.FindElements(By.ClassName("item")).Count;

            Assert.AreNotEqual(initCount, resCount);
        }

        [Test]
        public void ToggleFrame()
        {
            driver.FindElement(By.Id("togle_frame")).Click();
            var frame = driver.FindElement(By.TagName("iframe"));

            Thread.Sleep(2000);

            Assert.AreEqual(true, frame.Displayed);
        }

        private void CreateNewDriver()
        {
            driver.FindElement(By.Id("name")).SendKeys("Updated");
            driver.FindElement(By.Id("age")).SendKeys("20");
            driver.FindElement(By.Id("rang")).SendKeys("1");
            driver.FindElement(By.Id("submit")).Click();
            Thread.Sleep(2000);
        }

        [TearDown]
        public void Dispose()
        {
            driver.Close();
        }
    }
}
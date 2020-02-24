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
        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "http://localhost:3000/";
        }

        [TearDown]
        public void Dispose()
        {
            driver.Close();
        }
    }
}

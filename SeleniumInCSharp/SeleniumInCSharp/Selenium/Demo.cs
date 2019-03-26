using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Selenium
{
    class Demo
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            // string path = "C:\\mycode\\dotnet\\SeleniumInCSharp\\SeleniumInCSharp\\packages\\Selenium.Chrome.WebDriver.2.45\\driver\\";
            // driver = new ChromeDriver();\
            //string path = "C:\\mycode\\dotnet\\SeleniumInCSharp\\SeleniumInCSharp\\packages\\WebDriverIEDriver.2.45.0.0\\tools\\";
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            options.IgnoreZoomLevel = true;
            driver = new InternetExplorerDriver(options);
            // driver = new GecoDriver();

            driver.Url = "http://www.google.com";
            
            StringAssert.AreEqualIgnoringCase(driver.Title, "Google");
        }

        [Test]
        public void test()
        {
            
        }

        [TearDown]
        public void closeBrowser()
        {
          driver.Close();
        }
    }
}

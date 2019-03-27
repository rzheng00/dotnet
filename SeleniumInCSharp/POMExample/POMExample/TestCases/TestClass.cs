using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using POMExample.PageObjects;
using POMExample.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMExample
{   
    [TestFixture]
    class TestClass : BaseTestCase
    {
        //private IWebDriver driver;
        //protected ExtentReports _extent;
        //protected ExtentTest _test;

        [OneTimeSetUp]
        public void BeforeDerivedClass()
        {
            Console.WriteLine("OneTimeSetUp method, BeforeDerivedClass, called in child class");
            //try
            //{
            //    //To create report directory and add HTML report into it

            //    _extent = new ExtentReports();
            //    var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            //    DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");
            //    var htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports" + "\\Automation_Report" + ".html");
            //    _extent.AddSystemInfo("Environment", "Selenium NUnit ExtentReport");  
            //    _extent.AttachReporter(htmlReporter);
            //}
            //catch (Exception e)
            //{
            //    throw (e);
            //}
            
        }

        [SetUp]
        public void DerivedClassSetUp()
        {
            // driver = new ChromeDriver();

            driver = new InternetExplorerDriver();

            // driver = new FirefoxDriver();

            driver.Manage().Window.Maximize();
            driver.Url = "https://www.swtestacademy.com/";
           
            driver.Navigate().Refresh();
            Thread.Sleep(1000);
            StringAssert.AreEqualIgnoringCase(driver.Title, "Software Test Academy");

            //// Report
            //try
            //{
            //    _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //}
            //catch (Exception e)
            //{
            //    throw (e);
            //}
        }

        [Test, TestCaseSource("QueryData"),Order(1)]
        public void SearchTextFormAboutPage(string keyword, string matchedArticles)
        {
            HomePage home = new HomePage(driver);
            home.GetAboutLink().Click();
            StringAssert.AreEqualIgnoringCase(driver.Title, "About");
            AboutPage about = new AboutPage(driver);
            about.GetSearchIcon().Click();
            Assert.IsNotNull(about.GetSearchbox());
            about.Search(keyword);

            ResultPage result = new ResultPage(driver);
            int count = result.GetArticles().Count;
            Assert.AreEqual(count, Convert.ToInt32(matchedArticles));
            Console.WriteLine("counts: " + count);

            //while (result.HasMoreItems())
            //{
            //    Utilities.Utility.ExecuteJs(driver, "window.scrollTo(0, document.body.scrollHeight)");
            //    Console.WriteLine("need scroll down " + result.GetArticles().Count);
            //    Thread.Sleep(100);
            //}


        }

        //[Test, Order(2)]
        //public void LoadMoreResults()
        //{
        //    //ResultPage result = new ResultPage(driver);
        //    //int count = result.GetArticles().Count;
        //    //Assert.Greater(count, 0);
        //    //Console.WriteLine("counts: " + count);
        //}


        [TearDown]
        public void DerivedClassTearDown()
        {
            Console.WriteLine("TearDown method in Child is called");
            //try
            //{
            //    var status = TestContext.CurrentContext.Result.Outcome.Status;
            //    var stacktrace = "" +TestContext.CurrentContext.Result.StackTrace + "";
            //    var errorMessage = TestContext.CurrentContext.Result.Message;
            //    Status logstatus;
            //    switch (status)
            //    {
            //        case TestStatus.Failed:
            //            logstatus = Status.Fail;
            //            string screenShotPath = Utilities.Utility.Capture(driver, TestContext.CurrentContext.Test.Name);
            //            _test.Log(logstatus, "Test ended with " +logstatus + " – " +errorMessage);
            //            _test.Log(logstatus, "Snapshot below: " +_test.AddScreenCaptureFromPath(screenShotPath));
            //            break;
            //        case TestStatus.Skipped:
            //            logstatus = Status.Skip;
            //            _test.Log(logstatus, "Test ended with " +logstatus);
            //            break;
            //        default:
            //            logstatus = Status.Pass;
            //            _test.Log(logstatus, "Test ended with " +logstatus);
            //            break;
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw (e);
            //}

        }

        [OneTimeTearDown]
        public void AfterDerivedClass()
        {
            Console.WriteLine("AfterClass method in Child is called");
            //try
            //{
            //    _extent.Flush();
            //}
            //catch (Exception e)
            //{
            //    throw (e);
            //}
            //driver.Quit();
        }

        public static IEnumerable<TestCaseData> QueryData()
        {
            return Util.TestDataReader.ReadFromCSV("test2.csv");
        }
    }
}

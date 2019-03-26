using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExample.Utilities
{
    class BaseTestCase
    {
        protected static ExtentReports _extent;
        protected static ExtentTest _test;
        protected static ExtentHtmlReporter htmlReporter;

        protected static IWebDriver driver;

        [OneTimeSetUp]
        public void BeforeClass()
        {
            Console.WriteLine("BeforeClass method in base");
            try
            {
                //To create report directory and add HTML report into it
                if (htmlReporter == null && _extent == null)
                {

                    _extent = new ExtentReports();
                    var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                    DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");


                    htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports" + "\\Automation_Report.html");
                    //htmlReporter = new ExtentHtmlReporter("\\Automation_Report.html");
                    _extent.AddSystemInfo("Environment", "Selenium NUnit ExtentReport");
                    _extent.AttachReporter(htmlReporter);
                } else
                {
                    Console.WriteLine("Report object is init already");
                }
            }
            catch (Exception e)
            {
                throw (e);
            }

        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp method in base is called");
            // Report
            try
            {
                _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("TearDown method in base is called");
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
                var errorMessage = TestContext.CurrentContext.Result.Message + "\n" + stacktrace;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        string screenShotPath = Utilities.Utility.Capture(driver, TestContext.CurrentContext.Test.Name);
                        _test.Log(logstatus, "Test ended with " + logstatus + " – " + errorMessage);
                        _test.Log(logstatus, "Snapshot below: " + _test.AddScreenCaptureFromPath(screenShotPath));
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            Console.WriteLine("AfterClass method in base is called");
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}

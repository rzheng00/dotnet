using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace POMExample.Utilities
{
    static class Utility
    {
        public static IWebElement FindElementByJs(this IWebDriver driver, string jsCommand)
        {
            return (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(jsCommand);
        }

        public static IReadOnlyCollection<IWebElement> FindElementsByJs(this IWebDriver driver, string jsCommand)
        {
            return (IReadOnlyCollection<IWebElement>)((IJavaScriptExecutor)driver).ExecuteScript(jsCommand);
        }
       
        public static void ExecuteJs(this IWebDriver driver, string jsCommand)
        {
           ((IJavaScriptExecutor)driver).ExecuteScript(jsCommand);
        }

        public static T ExecuteJs<T>(this IWebDriver driver, string jsCommand)
        {
            return (T)((IJavaScriptExecutor)driver).ExecuteScript(jsCommand);
        }

        public static string Capture(this IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            string finalpath = "";
            try
            {
                Thread.Sleep(2000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
                DirectoryInfo di = Directory.CreateDirectory(dir + "\\Defect_Screenshots\\");
                finalpath = pth.Substring(0, pth.LastIndexOf("bin")) + "\\Defect_Screenshots\\" +screenShotName + ".png";
                localpath = new Uri(finalpath).LocalPath;
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return finalpath;
        }
    }
    /*
    public static IWebElement FindElementByJsWithWait(this IWebDriver driver, string jsCommand, int timeoutInSeconds)
    {
        if (timeoutInSeconds > 0)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d => d.FindElementByJs(jsCommand));
        }
        return driver.FindElementByJs(jsCommand);
    }

    public static IWebElement FindElementByJsWithWait(this IWebDriver driver, string jsCommand)
    {
        return FindElementByJsWithWait(driver, jsCommand, s_PageWaitSeconds);
    }
    */
}


using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace POMExample.PageObjects
{
    class HomePage : BasePageObject
    {
        // private IWebDriver driver;
        private IWebElement lnk_about;
        private IWebElement icon_search;

        public HomePage(IWebDriver driver) : base(driver) 
        {
            // this.driver = driver;
            Console.WriteLine("call constructure in child");
        }

        public IWebElement GetAboutLink()
        {
            this.lnk_about = driver.FindElement(By.LinkText("About"));

            return this.lnk_about;
        }
        
        public IWebElement GetSearchIcon()
        {
            this.icon_search = driver.FindElement(By.CssSelector("//a[title='search']"));
            return this.icon_search;
        }
    }
}

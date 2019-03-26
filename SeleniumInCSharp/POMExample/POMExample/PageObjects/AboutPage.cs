using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExample.PageObjects
{
    class AboutPage : BasePageObject
    {
        private IWebElement icon_search;
        private IWebElement div_searchbox;
        public AboutPage(IWebDriver driver) : base(driver)
        {

        }

        public IWebElement GetSearchIcon()
        {
            this.icon_search = driver.FindElement(By.ClassName("fusion-main-menu-icon"));
            return this.icon_search;
        }

        public IWebElement GetSearchbox()
        {
            this.div_searchbox = driver.FindElement(By.CssSelector("a[title='Search']"));
            return this.div_searchbox;
        }
        public void Search(string keyword)
        {
            driver.FindElement(By.Name("s")).SendKeys(keyword);
            driver.FindElement(By.CssSelector("input[class = 'fusion-search-submit searchsubmit']")).Click();
        }

    }
}

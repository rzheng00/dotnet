using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExample.PageObjects
{
    class BasePageObject
    {
        protected IWebDriver driver;
        public BasePageObject(IWebDriver driver)
        {
            this.driver = driver;
            Console.WriteLine("call constructure in parent");
        }
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExample.PageObjects
{
    class ResultPage : BasePageObject
    {
        private IReadOnlyCollection<IWebElement> articles;
        public ResultPage(IWebDriver driver) : base(driver)
        {

        }

        public IReadOnlyCollection<IWebElement> GetArticles()
        {
            this.articles = driver.FindElements(By.TagName("article"));
            return this.articles;
        }

        public Boolean HasMoreItems()
        {
            
            IReadOnlyCollection<IWebElement> messages = driver.FindElements(By.CssSelector("div > em"));
            foreach (IWebElement message in messages)
            {
                if (Equals(message.GetAttribute("textContent"), "All items displayed."))
                {
                    return false;
                } else if(Equals(message.GetAttribute("textContent"), "Loading the next set of posts..."))
                {
                    return true;
                } else
                {
                    Console.WriteLine("Can not find the emlement..." + message.GetAttribute("textContent"));                    
                }
            }
            return true;


        }
       
    }
}

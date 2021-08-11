using BookPriceComparasion.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace BookPriceComparasion.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private static ChromeOptions _chromeOptions;
        private string[] _urlList = {"https://www.amazon.com.tr/s?k=", "https://www.bkmkitap.com/arama?q=" };
        private string _query = "Otomatik Portakal";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _chromeOptions = new ChromeOptions();
            //var user_agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36";
            _chromeOptions.AddArgument("--window-size=1920,1080");
            _chromeOptions.AddArgument("--start-maximized");
            //_chromeOptions.AddArgument("--headless");
            using IWebDriver driver = new ChromeDriver(_chromeOptions);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            foreach (var url in _urlList)
            {
                driver.Navigate().GoToUrl(url + _query);
                //driver.FindElement(By.Name("q")).SendKeys("cheese" + Keys.Enter);
                if (url.Contains("amazon"))
                {
                    wait.Until(webDriver => webDriver.FindElement(By.CssSelector("img[class='s-image']")).Displayed);
                    IWebElement firstResult = driver.FindElement(By.CssSelector("img[class='s-image']"));
                    TempData["AmazonBookImage"] += firstResult.GetAttribute("src");
                }
                else
                {
                    wait.Until(webDriver => webDriver.FindElement(By.CssSelector("img[class*='stImage']")).Displayed);
                    IWebElement firstResult = driver.FindElement(By.CssSelector("img[class*='stImage']"));
                    TempData["BkmBookImage"] += firstResult.GetAttribute("src");
                }
                
            }
            

            return View("Index", "Hi");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

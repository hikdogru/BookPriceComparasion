using BookPriceComparasion.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace BookPriceComparasion.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private static ChromeOptions _chromeOptions;
        private static Thread _threadAmazon;
        private static Thread _threadKidega;
        private static Thread _threadBkm;
        private static WebDriverWait _wait;


        private readonly ILogger<HomeController> _logger;
        private string[] _urlList = { "https://www.amazon.com.tr/s?k=", "https://www.bkmkitap.com/arama?q=", "https://kidega.com/arama/", "https://www.idefix.com/search/?Q=" };
        private string _query = "Otomatik Portakal";


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //Stopwatch stopwatch = Stopwatch.StartNew();



            //foreach (var url in _urlList)
            //{
            //    //driver.FindElement(By.Name("q")).SendKeys("cheese" + Keys.Enter);
            //    if (url.Contains("amazon"))
            //    {
            //        _threadAmazon = new Thread(() =>
            //            FindElementAmazon(By.CssSelector("img[class='s-image']"), url, "Amazon"));
            //        _threadAmazon.Start();



            //    }
            //    else if (url.Contains("kidega"))
            //    {
            //        _threadKidega = new Thread(() =>
            //            FindElementKidega(By.CssSelector("img[class='lazyload activeted loadedImg']"), url, "Kidega"));
            //        _threadKidega.Start();

            //    }
            //    else
            //    {
            //        _threadBkm = new Thread(() => FindElementBkm(By.CssSelector("img[class*='stImage']"), url, "Bkm"));
            //        _threadBkm.Start();

            //    }
            //}



            //Debug.WriteLine("**************************************");
            //Debug.WriteLine($"Elapsed time {stopwatch.ElapsedMilliseconds} ");
            //stopwatch.Stop();

            return View("Index", "Hi");
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            if (!String.IsNullOrEmpty(query))
            {
                _query = query;
            }

            GetChromeOptions();
            _threadAmazon = new Thread(() =>
                FindElementAmazon(new[] { By.CssSelector("img[class='s-image']"), By.CssSelector("img[class=' lazyloaded']") }, new[] { _urlList[0], _urlList[3] },
                    new[] { "Amazon", "Idefix" }));
            _threadAmazon.Start();


            _threadKidega = new Thread(() =>
                FindElementKidega(new[] { By.CssSelector("img[class='lazyload activeted loadedImg']"), By.CssSelector("img[class*='stImage']") }, new[] { _urlList[2], _urlList[1] },
                    new[] { "Kidega", "Bkm" }));
            _threadKidega.Start();

            //_threadBkm = new Thread(() => FindElementBkm(By.CssSelector("img[class*='stImage']"), _urlList[1], "Bkm"));
            //_threadBkm.Start();

            _threadAmazon.Join();
            //_threadBkm.Join();
            _threadKidega.Join();



            return View("SearchResults");
        }

        public void GetChromeOptions()
        {
            _chromeOptions = new ChromeOptions();
            var userAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36";
            _chromeOptions.AddArgument("--window-size=1920,1080");
            _chromeOptions.AddArgument("--start-maximized");
            _chromeOptions.AddArgument($"user_agent={userAgent}");
            _chromeOptions.AddArgument("--ignore-certificate-errors");
            _chromeOptions.AddArgument("no-sandbox");
            _chromeOptions.AddArgument("--disable-gpu");
           //_chromeOptions.AddArgument("--headless");
        }

        public void FindElementAmazon(By[] by, string[] url, string[] websiteName)
        {

            for (int i = 0; i < by.Length; i++)
            {
                try
                {
                    using var driver = new ChromeDriver(_chromeOptions);
                    driver.Navigate().GoToUrl(url[i] + _query);
                    if (websiteName[i] == "Idefix") Thread.Sleep(1000);
                    _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                    _wait.Until(webDriver => webDriver.FindElement(by[i]).Displayed);
                    var elements = driver.FindElements(by[i]);

                    TempData[$"{websiteName[i]}BookImage"] = elements.Select(x => x.GetAttribute("src")).Take(10).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }


        public void FindElementBkm(By by, string url, string websiteName)
        {
            using var driver = new ChromeDriver(_chromeOptions);
            driver.Navigate().GoToUrl(url + _query);
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            _wait.Until(webDriver => webDriver.FindElement(by).Displayed);
            IWebElement firstResult = driver.FindElement(by);
            TempData["BkmBookImage"] = firstResult.GetAttribute("src");



        }

        public void FindElementKidega(By[] by, string[] url, string[] websiteName)
        {
            for (int i = 0; i < by.Length; i++)
            {
                using var driver = new ChromeDriver(_chromeOptions);
                driver.Navigate().GoToUrl(url[i] + _query);
                if (websiteName[i] == "Bkm")
                {
                    IJavaScriptExecutor js = driver;
                    //js.ExecuteScript("window.scrollTo(0, 400)");
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                    Thread.Sleep(1000);
                }
                _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                _wait.Until(webDriver => webDriver.FindElement(by[i]).Displayed);
                
                var elements = driver.FindElements(by[i]);
                TempData[$"{websiteName[i]}BookImage"] = elements.Select(x => x.GetAttribute("src")).Take(10).ToList();

            }


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

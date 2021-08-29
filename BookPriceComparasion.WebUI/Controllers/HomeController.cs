using BookPriceComparasion.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using OpenQA.Selenium.Interactions;

namespace BookPriceComparasion.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private static ChromeOptions _chromeOptions;
        private static Thread _thread1;
        private static Thread _thread2;
        private static List<BookViewModel> _tempBooks = new();

        private readonly List<Book> _books = new();
        private readonly ILogger<HomeController> _logger;
        private string _query = "";


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }


        [HttpGet]
        public IActionResult Search(string query)
        {
            if (_tempBooks.Where(b => b.Name.ToLower() == query.ToLower()).Count() == 0) _tempBooks.Clear();
            string fileName = $"{Directory.GetCurrentDirectory()}/Controllers/AllBookXPath.json";
            var file = System.IO.File.ReadAllText(fileName);
            var json = JsonConvert.DeserializeObject<List<ItemXPath>>(file);

            Stopwatch stopwatch = Stopwatch.StartNew();

            if (!String.IsNullOrEmpty(query))
            {
                _query = query;
            }

            GetChromeOptions();

            if (_tempBooks.Count == 0)
            {
                _thread2 = new Thread(() => FindElementsAllWebsitesPartOne(json?.Skip(2).Take(5).ToList()));
                _thread2.Start();

               

                _thread1 = new Thread(() =>
                    FindElementsAllWebsitesPartOne(json?.Skip(7).Take(5).ToList()));
                _thread1.Start();


                _thread2.Join();
                _thread1.Join();

                var bookGroupingByPublisher = _books.GroupBy(b => new { Publisher = b.Publisher.ToLower(), b.Name })
                                                .Select(b => new BookViewModel { Publisher = b.Key.Publisher, Name = b.Key.Name, Books = b.ToList() })
                                                .Where(b => b.Books.Count() > 1).ToList();
                TempData["Books"] = bookGroupingByPublisher;
                _tempBooks = bookGroupingByPublisher;
            }

            else
                TempData["Books"] = _tempBooks;


            Debug.WriteLine("**************************************");
            Debug.WriteLine($"Elapsed time {stopwatch.ElapsedMilliseconds} ");
            stopwatch.Stop();
            return View("SearchResults");
        }
        [HttpGet]
        public IActionResult GetComparedBooks(string publisher, string bookName)
        {
            var comparedBooks = _tempBooks?.Where(b => b.Publisher == publisher && b.Name == bookName).ToList();
            var orderedBooks = comparedBooks[0].Books.OrderBy(b => PriceConvertToDouble(b.Price)).ToList();
            return View("ComparedBookList", orderedBooks);
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
            _chromeOptions.AddArgument("--headless");
        }

      
        public void FindElementsAllWebsitesPartOne(List<ItemXPath> itemXPaths)
        {
            using var driver = new ChromeDriver(_chromeOptions);
            for (int i = 0; i < itemXPaths.Count; i++)
            {
                try
                {
                    driver.Navigate().GoToUrl(itemXPaths[i].WebsiteSearchUrl + _query);
                    if (itemXPaths[i].WebsiteName == "Idefix") Thread.Sleep(1000);
                    IJavaScriptExecutor js = driver;
                    js.ExecuteScript("window.scrollTo(0, 1000)");
                    Thread.Sleep(1000);
                    if (itemXPaths[i].WebsiteName == "Kitapyurdu")
                    {
                        bool isElementExist = IsElementPresent(driver, By.XPath("//input[@name='selected_in_stock']"));
                        if (isElementExist)
                        {
                            var element = driver.FindElement(By.XPath("//input[@name='selected_in_stock']"));
                            Actions action = new Actions(driver);
                            action.MoveToElement(element).Click().Build().Perform();
                        }

                    }
                    if (IsElementPresent(driver, By.XPath(itemXPaths[i].Image)))
                        FindElements(driver, itemXPaths[i]);    
                }
                catch (Exception e)
                {
                }
            }
        }
        private void FindElements(IWebDriver driver, ItemXPath itemXPath)
        {
            var names = driver.FindElements(By.XPath(itemXPath.Name));
            var authors = driver.FindElements(By.XPath(itemXPath.Author));
            var publishers = driver.FindElements(By.XPath(itemXPath.Publisher));
            var prices = driver.FindElements(By.XPath(itemXPath.Price));
            var images = driver.FindElements(By.XPath(itemXPath.Image));
            var detailUrls = driver.FindElements(By.XPath(itemXPath.DetailLink));
            string website = itemXPath.WebsiteName;
            var booksListViewModel = new BooksListViewModel { Authors = authors, Images = images, Names = names, Prices = prices, Publishers = publishers, Website = website, DetailUrls = detailUrls, WebsiteLogo = itemXPath.WebsiteLogo };
            SaveToList(booksListViewModel, itemXPath);

        }


        private void SaveToList(BooksListViewModel booksListViewModel, ItemXPath itemXPath)
        {
            int lastNumber = booksListViewModel.Names.Count > 15 ? 15 : booksListViewModel.Names.Count;
            for (int i = 0; i < lastNumber; i++)
            {
                var book = new Book
                {
                    Id = i + 1,
                    Author = booksListViewModel.Authors[i].GetAttribute("innerText"),
                    Image = booksListViewModel.Images[i].GetAttribute("src"),
                    Name = booksListViewModel.Names[i].GetAttribute("innerText"),
                    Price = booksListViewModel.Prices[i].GetAttribute("innerText"),
                    Publisher = booksListViewModel.Publishers.Count == 0 ? "" : booksListViewModel.Publishers[i].GetAttribute("innerText"),
                    DetailUrl = booksListViewModel.DetailUrls[i].GetAttribute("href"),
                    WebsiteName = booksListViewModel.Website,
                    WebsiteLogo = booksListViewModel.WebsiteLogo,
                };

                if (booksListViewModel.Website == "Eganba") book.Price = book.Price.Split(' ')[1];
                if (booksListViewModel.Website == "Pandora")
                    book.Price = book.Price.Replace("Site Fiyatı:", "");
                if (booksListViewModel.Website == "Bkm")
                    book.Price = book.Price.Replace("Sepete Ekle", "");

                if (IsPriceValid(book.Price))
                    _books.Add(book);
            }
        }

       
        private static double PriceConvertToDouble(string price)
        {
            string replacedPrice = price
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("₺", "")
                .Replace("TL", "")
                .Replace(",", ".")
                .Trim();
            return Convert.ToDouble(replacedPrice);
        }


        private static bool IsPriceValid(string price)
        {
            if (price.Contains("TL") || price.Contains("₺"))
                return true;


            return false;
        }
        private bool IsElementPresent(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

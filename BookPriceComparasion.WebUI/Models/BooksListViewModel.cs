using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookPriceComparasion.WebUI.Models
{
    public class BooksListViewModel
    {
        public ReadOnlyCollection<IWebElement> Names { get; set; }
        public ReadOnlyCollection<IWebElement> Authors { get; set; }
        public ReadOnlyCollection<IWebElement> Publishers { get; set; }
        public ReadOnlyCollection<IWebElement> Images { get; set; }
        public ReadOnlyCollection<IWebElement> Prices { get; set; }
        public ReadOnlyCollection<IWebElement> DetailUrls { get; set; }
        public string Website { get; set; }
        public string WebsiteLogo { get; set; }
    }
}

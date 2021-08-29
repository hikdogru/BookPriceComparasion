using OpenQA.Selenium;

namespace Selenium_Web_Scraping
{
    public class BookXPath
    {
        public By Name { get; set; }
        public By Author { get; set; }
        public By Price { get; set; }
        public By Publisher { get; set; }
        public By DetailLink { get; set; }
        public By Image { get; set; }
        public string WebsiteName { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPriceComparasion.WebUI.Models
{
    public class BookViewModel
    {
        public string Name { get;  set; }
        public string Publisher { get;  set; }
        public List<Book> Books { get; set; }
        
    }
}

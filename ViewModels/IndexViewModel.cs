using System.Collections.Generic;
using ProductShop.Models;

namespace ProductShop.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<SuperMarket> SuperMarkets { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ViewModels.ProductViewModels
{
    public class NewProductViewModel
    {
        public string ProductName { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string ProductDescription { get; set; }
        public decimal SellPrice { get; set; }
        public decimal LendPrice { get; set; }
        public int MinLendDays { get; set; }
        public string MainPhotoUrl { get; set; }

        public bool Sell { get; set; }
        public bool Lend { get; set; }
    }
}


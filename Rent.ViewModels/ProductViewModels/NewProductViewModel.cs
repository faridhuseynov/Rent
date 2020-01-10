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
        public decimal ProductPrice { get; set; }
    }
}


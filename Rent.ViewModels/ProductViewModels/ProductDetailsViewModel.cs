using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ViewModels.ProductViewModels
{
    public class ProductDetailsViewModel
    {
        public string ProductName { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}

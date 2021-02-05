﻿using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ViewModels.ProductViewModels
{
    public class EditProductDetailsViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }

        public decimal SellPrice { get; set; }
        public decimal LendPrice { get; set; }
        public int MinLendDays { get; set; }
        public string ProductDescription { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }
        public string MainPhotoUrl { get; set; }
        public bool Sell { get; set; }
        public bool Lend { get; set; }
        public int CurrentlyRented { get; set; }
        public int TotalAmount { get; set; }
    }
}

using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rent.ViewModels.ProductViewModels
{
    public class ProductDetailsViewModel
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal SellPrice { get; set; }
        public decimal LendPrice { get; set; }
        public int MinLendDays { get; set; }
        public string ProductDescription { get; set; }
        public bool Blocked { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        //think about deleting the CategoryID!!!!!!!!!!!!!!!!!!!!!!!!!
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public string MainPhotoUrl { get; set; }
        public bool Sell { get; set; }
        public bool Lend { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public double AverageRate { get; set; }
        public int CurrentlyRented { get; set; }
        public int TotalAmount { get; set; }

    }
}

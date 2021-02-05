using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal SellPrice { get; set; }
        public decimal LendPrice { get; set; }
        [Range(1,double.PositiveInfinity,ErrorMessage ="Minimum kirayə müddəti 1 gün olmalıdır")]
        public int MinLendDays { get; set; }
        public string ProductDescription { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public string MainPhotoUrl { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
        public bool Sell { get; set; }
        public bool Lend { get; set; }
        public virtual ICollection<WishListProduct> WishList { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public double AverageRate { get; set; }
        public bool Blocked { get; set; }
        public int TotalAmount { get; set; }
        public int CurrentlyRented { get; set; }

    }
}

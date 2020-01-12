using System;
using System.Collections.Generic;
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
        public string ProductDescription { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public string MainPhotoUrl { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
        public bool Sell { get; set; }
        public bool Lend { get; set; }

    }
}

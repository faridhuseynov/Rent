using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class Subcagetory
    {
        public int Id { get; set; }
        public string SubategoryName { get; set; }
        public string SubategoryDescription { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}

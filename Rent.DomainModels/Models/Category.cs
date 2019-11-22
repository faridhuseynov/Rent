using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

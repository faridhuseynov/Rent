using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}

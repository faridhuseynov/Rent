using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ViewModels.SubcategoryViewModels
{
    public class SubcategoryDetailsViewModel
    {
        public string SubcategoryName { get; set; }
        public string SubcategoryDescription { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}

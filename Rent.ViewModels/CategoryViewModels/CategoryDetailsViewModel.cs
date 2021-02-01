using Rent.DomainModels.Models;
using Rent.ViewModels.SubcategoryViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ViewModels.CategoryViewModels
{
    public class CategoryDetailsViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}

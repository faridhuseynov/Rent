using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ViewModels.CategoryViewModels
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        //to be later added for the update
        //public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }

    }
}

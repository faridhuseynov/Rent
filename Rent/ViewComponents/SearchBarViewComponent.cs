using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rent.DomainModels.Models;
using Rent.ServiceLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rent.ViewComponents
{
    public class SearchBarViewComponent:ViewComponent
    {
        private readonly ICategoriesService cs;

        public SearchBarViewComponent(ICategoriesService cs)
        {
            this.cs = cs;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Category> list;
            list = await cs.GetCategories();
            return View(list);
        }
    }
}

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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //List<Category> categories =null;
            //try
            //{
            //    var json = HttpContext.Session.GetString("categories");
            //    categories = JsonConvert.DeserializeObject<List<Category>>(json);
            //}
            //catch (Exception)
            //{

            //}
            //ViewBag.Categories = categories;
            List<string> list = new List<string>();
            list.Add("First");
            list.Add("Second");
            list.Add("Third");
            ViewBag.Categories = list;
            return View();
        }
    }
}

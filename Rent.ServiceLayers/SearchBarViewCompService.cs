using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ServiceLayers
{
    public class SearchBarViewCompService
    {
        static public IEnumerable<Category> GetCategories(ISession session,List<Category> categories)
        {
            List<Category> categs;
            try
            {
                var sessionProducts = session.GetString("categories");
                categs = JsonConvert.DeserializeObject<List<Category>>(sessionProducts);
            }
            catch (Exception)
            {
                categs = new List<Category>(categories);
            }
            return categs;
        }
    }
}

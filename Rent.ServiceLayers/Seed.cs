using Newtonsoft.Json;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rent.ServiceLayers
{
    public class Seed
    {
        private readonly AppDbContext _context;
        public Seed(AppDbContext context)
        {
            _context = context;
        }

        public static void SeedProducts(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categoryData = System.IO.File.ReadAllText("categorydata.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("userdata.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
            if (!context.Products.Any())
            {
                var productData = System.IO.File.ReadAllText("data.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                foreach (var product in products)
                {
                    context.Products.Add(product);
                }

                context.SaveChanges();
            }
        }
    }
}

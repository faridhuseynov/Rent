using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface IProductsRepository
    {
        Task<int> AddProduct(Product product);
        Task UpdateProductDetails(Product product);
        //void UpdateUserPassword(User user);
        Task DeleteProduct(int productId);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductByProductID(int productId);
        Task<int> GetLatestProductID();
        Task UpdateRating(int productId);
    }
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext db;
        public ProductsRepository(AppDbContext dbContext)
        {
            db = dbContext;
        }
        async public Task<int> AddProduct(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await db.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product != null)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> GetLatestProductID()
        {
            //var product = await db.Products.LastOrDefaultAsync();
            //return product.Id;
            return 1;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await db.Products.ToListAsync();
            return products;
        }

        //public async Task<IEnumerable<User>> GetUsersByEmail(string email)
        //{
        //    var user = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
        //}

        //public IEnumerable<User> GetUsersByEmailAndPassword(string email, string passwordHash)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Product> GetProductByProductID(int productID)
        {
            return await db.Products.Include(u=>u.User).Include(i=>i.Images).FirstOrDefaultAsync(x => x.Id == productID);
        }

        public async Task UpdateProductDetails(Product product)
        {
            var updatedProduct = await db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (updatedProduct != null)
            {
                updatedProduct.ProductName = product.ProductName;
                updatedProduct.ProductDescription = product.ProductDescription;
                updatedProduct.Sell = product.Sell;
                updatedProduct.SellPrice = product.SellPrice;
                updatedProduct.Lend = product.Lend;
                updatedProduct.LendPrice = product.LendPrice;
                updatedProduct.MinLendDays = product.MinLendDays;
                updatedProduct.CategoryId = product.CategoryId;
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateRating(int productId)
        {
           var product = await db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product!=null)
            {
                int sum = 0;
                foreach (var item in product.Rates)
                {
                    sum += item.Note;
                }
                product.AverageRate = sum / product.Rates.Count;
                await db.SaveChangesAsync();
            }
        }

        //public void UpdateUserPassword(User user)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

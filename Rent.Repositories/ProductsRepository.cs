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
        void AddProduct(Product product);
        void UpdateProductDetails(Product product);
        //void UpdateUserPassword(User user);
        void DeleteProduct(int productId);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductByProductID(int productId);
        Task<int> GetLatestProductID();
    }
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext db;
        public ProductsRepository(AppDbContext dbContext)
        {
            db = dbContext;
        }
        async public void AddProduct(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        async public void DeleteProduct(int productId)
        {
            var product = await db.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product != null)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }

        async public Task<int> GetLatestProductID()
        {
            var product = await db.Products.LastAsync();
            return product.Id;
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
            return await db.Products.FirstOrDefaultAsync(x => x.Id == productID);
        }

        public async void UpdateProductDetails(Product product)
        {
            var updatedProduct = await db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (updatedProduct != null)
            {
                updatedProduct.ProductName = product.ProductName;
                await db.SaveChangesAsync();
            }
        }

        //public void UpdateUserPassword(User user)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

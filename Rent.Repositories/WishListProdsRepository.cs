using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface IWishListProdsRepository
    {
        Task InsertProduct(WishListProduct wishListProduct);

        Task DeleteProduct(WishListProduct wishListProduct);

        Task<IEnumerable<WishListProduct>> GetAllWishListProducts();

        Task<IEnumerable<WishListProduct>> GetWishListProducts(string userName);
    }
    public class WishListProdsRepository : IWishListProdsRepository
    {
        private readonly AppDbContext dbContext;

        public WishListProdsRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task InsertProduct(WishListProduct wishListProduct)
        {
            await dbContext.WishListProducts.AddAsync(wishListProduct);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteProduct(WishListProduct wishListProduct)
        {
            var productToRemove = await dbContext.WishListProducts.FirstOrDefaultAsync(w => w.Id == wishListProduct.Id);
            dbContext.WishListProducts.Remove(productToRemove);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<WishListProduct>> GetAllWishListProducts()
        {
            return await dbContext.WishListProducts.ToListAsync();
        }

        public async Task<IEnumerable<WishListProduct>> GetWishListProducts(string userName)
        {
            return await dbContext.WishListProducts.Where(u=>u.User.UserName==userName).ToListAsync();
        }

    }
}

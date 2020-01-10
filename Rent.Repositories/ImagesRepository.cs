using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface IImagesRepository
    {
        Task AddImage(ProductImage image);
        Task DeleteImage(int imageId);
        Task<IEnumerable<ProductImage>> GetImages();
        Task<IEnumerable<ProductImage>> GetImagesByProductID(int productId);
    }
    public class ImagesRepository:IImagesRepository
    {
        private readonly AppDbContext dbContext;

        public ImagesRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddImage(ProductImage image)
        {
            await dbContext.ProductImages.AddAsync(image);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteImage(int imageId)
        {
            var photo = await dbContext.ProductImages.FirstOrDefaultAsync(i => i.Id == imageId);
            if (photo!=null)
            {
                dbContext.ProductImages.Remove(photo);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductImage>> GetImages()
        {
           return await dbContext.ProductImages.ToListAsync();
        }

        public async Task<IEnumerable<ProductImage>> GetImagesByProductID(int productId)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product!=null)
            {
                return await dbContext.ProductImages.Where(p => p.ProductId == productId).ToListAsync();
            }
            return null;
        }
    }
}

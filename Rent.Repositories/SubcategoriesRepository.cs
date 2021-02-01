using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface ISubcategoriesRepository
    {
        Task<IEnumerable<Subcategory>> GetSubcategories();
        Task<Subcategory> GetSubcategoryById(int subcategoryId);
        Task<int> AddSubcategory(Subcategory subcagetory);
        Task UpdateSubcategory(Subcategory subcategory);
        Task RemoveSubcategory(int subcagetoryId);
        
    }
    public class SubcategoriesRepository : ISubcategoriesRepository
    {
        private readonly AppDbContext dbContext;

        public SubcategoriesRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> AddSubcategory(Subcategory subcagetory)
        {
            await dbContext.AddAsync(subcagetory);
            await dbContext.SaveChangesAsync();
            return subcagetory.Id;
        }

        public async Task<IEnumerable<Subcategory>> GetSubcategories()
        {
            var subcategories = await dbContext.Subcagetories.ToListAsync();
            return subcategories;
        }
        public async Task<Subcategory> GetSubcategoryById(int subcategoryId)
        {
            var subcategory = await dbContext.Subcagetories
                .FirstOrDefaultAsync(s => s.Id == subcategoryId);
            return subcategory;
        }
        public async Task UpdateSubcategory(Subcategory subcategory)
        {
            var subcat = await dbContext.Subcagetories
                .FirstOrDefaultAsync(s => s.Id == subcategory.Id);
            if (subcat!=null)
            {
                subcat.SubcategoryName = subcategory.SubcategoryName;
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task RemoveSubcategory(int subcagetoryId)
        {
            var subcategory = await dbContext.Subcagetories
                .FirstOrDefaultAsync(s => s.Id == subcagetoryId);
            if (subcategory!=null)
            {
                dbContext.Remove(subcategory);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

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
        Task<IEnumerable<Subcagetory>> GetSubcategories();
        Task<int> AddSubcategory(Subcagetory subcagetory);
        Task RemoveSubcategory(int subcagetoryId);
    }
    public class SubcategoriesRepository : ISubcategoriesRepository
    {
        private readonly AppDbContext dbContext;

        public SubcategoriesRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> AddSubcategory(Subcagetory subcagetory)
        {
            await dbContext.AddAsync(subcagetory);
            await dbContext.SaveChangesAsync();
            return subcagetory.Id;
        }

        public async Task<IEnumerable<Subcagetory>> GetSubcategories()
        {
            var subcategories = await dbContext.Subcagetories.ToListAsync();
            return subcategories;
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

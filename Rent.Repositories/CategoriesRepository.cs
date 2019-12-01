using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetCategories();

    }
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext db;
        public CategoriesRepository(AppDbContext dbContext)
        {
            db = dbContext;
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await db.Categories.ToListAsync();
            return categories;
        }
    }
}

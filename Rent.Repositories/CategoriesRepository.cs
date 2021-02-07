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
        Task<string> AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task<Category> GetCategoryById(int categoryId);        
        Task DeleteCategory(Category category);
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
        public async Task<string> AddCategory(Category category)
        {
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
            return category.CategoryName;
        }
        public async Task UpdateCategory(Category category)
        {
            var categoryFromRepo = await db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryFromRepo!=null)
            {
                categoryFromRepo.CategoryName = category.CategoryName;
                categoryFromRepo.CategoryDescription = category.CategoryDescription;
                await db.SaveChangesAsync();
            }
        }
        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            return category;
        }

        public async Task DeleteCategory(Category category)
        {
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
        }
    }
}

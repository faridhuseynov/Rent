using AutoMapper;
using Rent.DomainModels.Models;
using Rent.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rent.ViewModels.CategoryViewModels;

namespace Rent.ServiceLayers
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<CategoryDetailsViewModel>> GetCategoriesForReview();
        Task<string> AddCategory(NewCategoryViewModel newCategoryViewModel);
        Task<CategoryDetailsViewModel> GetCategoryById(int categoryId);
        Task RemoveCategory(int categoryId);
    }
    public class CategoriesService : ICategoriesService
    {
        public readonly ICategoriesRepository cr;
        public CategoriesService(ICategoriesRepository categoriessRepository)
        {
            this.cr = categoriessRepository;
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await cr.GetCategories();
        }

        public async Task<string> AddCategory(NewCategoryViewModel newCategoryViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewCategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category newCategory = mapper.Map<NewCategoryViewModel, Category>(newCategoryViewModel);
            return await cr.AddCategory(newCategory);
        }

        public async Task<IEnumerable<CategoryDetailsViewModel>> GetCategoriesForReview()
        {
            var categoryList = await cr.GetCategories(); 
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Category, CategoryDetailsViewModel>(); cfg.IgnoreUnmapped(); 
            });
            IMapper mapper = config.CreateMapper();
            var categories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDetailsViewModel>>(categoryList);
            return categories;
        }

        public async Task<CategoryDetailsViewModel> GetCategoryById(int categoryId)
        {
            var category = await cr.GetCategoryById(categoryId);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDetailsViewModel>(); cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            var categoryDetailViewModel = mapper.Map<Category, CategoryDetailsViewModel>(category);
            return categoryDetailViewModel;
        }
        public async Task RemoveCategory(int categoryId)
        {
            var category = await cr.GetCategoryById(categoryId);
            if (category!=null)
            {
                await cr.DeleteCategory(category);
            }
        }
    }
}

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
        Task<IEnumerable<NewCategoryViewModel>> GetCategoriesForReview();
        Task<string> AddCategory(NewCategoryViewModel newCategoryViewModel);
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

        public async Task<IEnumerable<NewCategoryViewModel>> GetCategoriesForReview()
        {
            var categoryList = await cr.GetCategories(); 
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, NewCategoryViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var categories = mapper.Map<IEnumerable<Category>, IEnumerable<NewCategoryViewModel>>(categoryList);
            return categories;
        }

    }
}

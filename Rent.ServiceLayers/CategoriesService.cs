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
    }
}

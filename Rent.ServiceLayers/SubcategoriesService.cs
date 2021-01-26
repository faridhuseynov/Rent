using AutoMapper;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ViewModels.SubcategoryViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers
{
    public interface ISubcategoriesService
    {
        Task<int> InsertSubcategory(NewSubcategoryViewModel nsvm);
        Task DeleteSubcategory(int subcategoryId);
        Task<IEnumerable<SubcategoryDetailsViewModel>> GetSubcagetories();

    }
    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly ISubcategoriesRepository subcategoriesRepository;

        public SubcategoriesService(ISubcategoriesRepository subcategoriesRepository)
        {
            this.subcategoriesRepository = subcategoriesRepository;
        }
        public async Task<int> InsertSubcategory(NewSubcategoryViewModel nsvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewSubcategoryViewModel, Subcagetory>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Subcagetory newSubcategory = mapper.Map<NewSubcategoryViewModel, Subcagetory>(nsvm);
            return await subcategoriesRepository.AddSubcategory(newSubcategory);
        }
        public async Task DeleteSubcategory(int subcategoryId)
        {
            await subcategoriesRepository.RemoveSubcategory(subcategoryId);
        }

        public async Task<IEnumerable<SubcategoryDetailsViewModel>> GetSubcagetories()
        {
            var subcategsList = await subcategoriesRepository.GetSubcategories();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Subcagetory, SubcategoryDetailsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var subcategories = mapper.Map<IEnumerable<Subcagetory>, IEnumerable<SubcategoryDetailsViewModel>>(subcategsList);
            return subcategories;
        }
    }
}

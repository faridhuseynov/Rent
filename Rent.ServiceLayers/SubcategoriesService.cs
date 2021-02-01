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
        Task<IEnumerable<SubcategoryDetailsViewModel>> GetSubcagetories();
        Task<SubcategoryDetailsViewModel> GetSubcategoryById(int subcategoryId);
        Task<int> InsertSubcategory(NewSubcategoryViewModel nsvm);
        Task UpdateSubcategory(EditSubcategoryViewModel esvm);
        Task DeleteSubcategory(int subcategoryId);

    }
    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly ISubcategoriesRepository subcategoriesRepository;

        public SubcategoriesService(ISubcategoriesRepository subcategoriesRepository)
        {
            this.subcategoriesRepository = subcategoriesRepository;
        }
        public async Task<IEnumerable<SubcategoryDetailsViewModel>> GetSubcagetories()
        {
            var subcategsList = await subcategoriesRepository.GetSubcategories();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Subcategory, SubcategoryDetailsViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            var subcategories = mapper.Map<IEnumerable<Subcategory>, IEnumerable<SubcategoryDetailsViewModel>>(subcategsList);
            return subcategories;
        }

        public async Task<SubcategoryDetailsViewModel> GetSubcategoryById(int subcategoryId)
        {
            var subcategory = await subcategoriesRepository.GetSubcategoryById(subcategoryId);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Subcategory, SubcategoryDetailsViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            var sdvm = mapper.Map<Subcategory, SubcategoryDetailsViewModel>(subcategory);
            return sdvm;
        }

        public async Task<int> InsertSubcategory(NewSubcategoryViewModel nsvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewSubcategoryViewModel, Subcategory>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Subcategory newSubcategory = mapper.Map<NewSubcategoryViewModel, Subcategory>(nsvm);
            return await subcategoriesRepository.AddSubcategory(newSubcategory);
        }

        public async Task UpdateSubcategory(EditSubcategoryViewModel esvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditSubcategoryViewModel, Subcategory>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Subcategory editSubcategory = mapper.Map<EditSubcategoryViewModel, Subcategory>(esvm);
            await subcategoriesRepository.UpdateSubcategory(editSubcategory);
        }

        public async Task DeleteSubcategory(int subcategoryId)
        {
            await subcategoriesRepository.RemoveSubcategory(subcategoryId);
        }

    }
       
}

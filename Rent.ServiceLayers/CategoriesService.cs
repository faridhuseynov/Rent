using Rent.DomainModels.Models;
using Rent.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetCategories();
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
    }
}

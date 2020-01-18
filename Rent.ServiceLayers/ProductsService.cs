using AutoMapper;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers
{
    public interface IProductsService
    {
        Task<int> InsertProduct(NewProductViewModel newProductViewModel);
        void UpdateProductDetails(EditProductDetailsViewModel productDetailsViewModel);
        Task DeleteProduct(int ProductID);
        IEnumerable<ProductDetailsViewModel> GetProducts();
        Task<ProductDetailsViewModel> GetProductByProductID(int ProductID);
        Task<int> GetLastAddedProductId();
    }

    public class ProductsService:IProductsService
    {
        public readonly IProductsRepository pr;
        public ProductsService(IProductsRepository productsRepository)
        {
            this.pr = productsRepository;
        }

        public async Task<int> InsertProduct(NewProductViewModel newProductViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewProductViewModel, Product>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Product newProduct = mapper.Map<NewProductViewModel, Product>(newProductViewModel);
            return await pr.AddProduct(newProduct);
        }

        public async Task DeleteProduct(int ProductID)
        {
            //var productFromRepo = pr.GetProductByProductID(ProductID);
            //if (productFromRepo!=null)
              await pr.DeleteProduct(ProductID);
        }

        public void UpdateProductDetails(EditProductDetailsViewModel editedProductDetailsViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditProductDetailsViewModel, Product>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Product product = mapper.Map<EditProductDetailsViewModel, Product>(editedProductDetailsViewModel);
            pr.UpdateProductDetails(product);
        }

        public IEnumerable<ProductDetailsViewModel> GetProducts()
        {
            var productsList = pr.GetProducts();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductDetailsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var products = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailsViewModel>>(productsList.Result);
            return products;
        }

        public async Task<ProductDetailsViewModel> GetProductByProductID(int ProductID)
        {
            var productFromRepo = await pr.GetProductByProductID(ProductID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductDetailsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var product = mapper.Map<Product, ProductDetailsViewModel>(productFromRepo);
            return  product;
        }

        public async Task<int> GetLastAddedProductId()
        {
            return await pr.GetLatestProductID();
        }

    }
}

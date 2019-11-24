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
        void DeleteProduct(int ProductID);
        IEnumerable<ProductDetailsViewModel> GetProducts();
        ProductDetailsViewModel GetProductByProductID(int ProductID);
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
            pr.AddProduct(newProduct);
            return await pr.GetLatestProductID();
        }

        public void DeleteProduct(int ProductID)
        {
            var productFromRepo = pr.GetProductByProductID(ProductID);
            if (productFromRepo!=null)
                pr.DeleteProduct(ProductID);
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

        public ProductDetailsViewModel GetProductByProductID(int ProductID)
        {
            var productFromRepo = pr.GetProductByProductID(ProductID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Product, ProductDetailsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var product = mapper.Map<Product, ProductDetailsViewModel>(productFromRepo.Result);
            return product;
        }

    }
}

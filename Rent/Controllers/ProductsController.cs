using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ServiceLayers;
using Rent.ViewModels.ProductViewModels;
using Rent.ViewModels.SubcategoryViewModels;

namespace Rent.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<User> userManager;
        private readonly IImagesRepository imagesRepository;
        private readonly ISubcategoriesService subcategoriesService;

        public ProductsController(AppDbContext context, IProductsService productsService,
            ICategoriesService categoriesService, UserManager<User> userManager,
            IImagesRepository imagesRepository, ISubcategoriesService subcategoriesService)
        {
            _context = context;
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.imagesRepository = imagesRepository;
            this.subcategoriesService = subcategoriesService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = productsService.GetProducts().Where(u => u.User.UserName == User.Identity.Name).Where(p => p.Blocked != true);
            ViewBag.Categories = await categoriesService.GetCategories();
            return View(products);
        }

        // GET: Products/Blocked
        public async Task<IActionResult> Blocked()
        {
            var products = productsService.GetProducts().Where(u => u.User.UserName == User.Identity.Name).Where(p => p.Blocked == true);
            ViewBag.Categories = await categoriesService.GetCategories();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Subcagetory)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoriesService.GetCategories().Result, "Id", "CategoryName");
            var randomCatId = categoriesService.GetCategories().Result.First().Id;
            ViewData["SubcategoryId"] = new SelectList(subcategoriesService.GetSubcategoriesByCategoryId(randomCatId).Result, "Id", "SubcategoryName");


            ViewData["UserId"] = userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            return View();
        }
        
        [HttpGet]
        public async Task<SelectList> GetUpdatedSubcategoryList(int categoryId)
        {
            var newList = new SelectList(await subcategoriesService.GetSubcategoriesByCategoryId(categoryId), "Id", "SubcategoryName");
            return newList;
        }
        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IEnumerable<IFormFile> images, [Bind("Id,ProductName,ProductPrice,TotalAmount, ProductDescription,UserId,SubcategoryId,Sell, Lend, MinLendDays, SellPrice, LendPrice")] NewProductViewModel newProductViewModel)
        {
            string fileName = "";
            var fileNames = new List<string>();
            if (ModelState.IsValid)
            {
                if (images != null)
                {
                    try
                    {
                        foreach (var image in images)
                        {
                            fileName = FileUploaderService.UploadProductPhoto(image);
                            fileNames.Add(fileName);
                        }
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }


                var newProdId = await productsService.InsertProduct(newProductViewModel);

                if (newProdId > 0)
                {
                    if (fileNames != null)
                    {
                        foreach (var filePath in fileNames)
                        {
                            await imagesRepository.AddImage(new ProductImage { PhotoUrl = filePath, ProductId = newProdId });

                        }
                        if (productsService.GetProductByProductID(newProdId).Result.MainPhotoUrl == null)
                        {
                            await imagesRepository.SetMainPhoto(newProdId, fileNames[0]);
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoriesService.GetCategories().Result, "Id", "CategoryName");
            ViewData["UserId"] = userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            return View(newProductViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productsService.GetProductByProductID(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(categoriesService.GetCategories().Result, "Id", "CategoryName");
            ViewData["UserId"] = userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IEnumerable<IFormFile> images, [Bind("Id,ProductName,ProductDescription,TotalAmount,Sell,Lend, SellPrice, LendPrice, MinLendDays, CategoryId")] ProductDetailsViewModel product)
        {
            if (ModelState.IsValid)
            {
                var checkProduct = await productsService.GetProductByProductID(product.Id);
                if (checkProduct != null)
                {
                    
                    try
                    {
                        await productsService.UpdateProductDetails(product);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    if (images.Count() > 0)
                    {
                        string fileName = "";
                        var fileNames = new List<string>();
                        try
                        {
                            foreach (var image in images)
                            {
                                fileName = FileUploaderService.UploadProductPhoto(image);
                                fileNames.Add(fileName);
                            }
                        }
                        catch (Exception)
                        {
                            return BadRequest();
                        }

                        if (fileNames != null)
                        {
                            foreach (var filePath in fileNames)
                            {
                                await imagesRepository.AddImage(new ProductImage { PhotoUrl = filePath, ProductId = checkProduct.Id });

                            }
                            if (checkProduct.MainPhotoUrl == null)
                            {
                                await imagesRepository.SetMainPhoto(checkProduct.Id, fileNames[0]);
                            }
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoriesService.GetCategories().Result, "Id", "CategoryName");
            ViewData["UserId"] = userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            return View(product);
        }

        // GET: Products/Delete/ProductId
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var product = await productsService.GetProductByProductID(Id);
            if (product != null && (product.User.UserName == User.Identity.Name || User.Identity.Name == "admin@rent.com"))
            {
                await productsService.DeleteProduct(product.Id);
                return RedirectToAction("Index", "Products");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task SetMainPhoto(int productId, string filename)
        {
            var product = await productsService.GetProductByProductID(productId);
            if (product != null && !String.IsNullOrWhiteSpace(filename) && (product.User.UserName == User.Identity.Name || User.Identity.Name == "admin@rent.com"))
            {
                await imagesRepository.SetMainPhoto(productId, filename);
            }
        }

        [HttpPost]
        public async Task DeletePhoto(int photoId)
        {
            await imagesRepository.DeleteImage(photoId);
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

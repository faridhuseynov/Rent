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

        public ProductsController(AppDbContext context, IProductsService productsService,
            ICategoriesService categoriesService, UserManager<User> userManager, IImagesRepository imagesRepository)
        {
            _context = context;
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.imagesRepository = imagesRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = productsService.GetProducts().Where(u=>u.User.UserName== User.Identity.Name);
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
                .Include(p => p.Category)
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
            ViewData["UserId"] = userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile image, [Bind("Id,ProductName,ProductPrice,ProductDescription,UserId,CategoryId,Sell, Lend")] NewProductViewModel newProductViewModel)
        {
            string fileName="";
            if (ModelState.IsValid)
            {
                if (image!=null)
                {
                    try
                    {
                        fileName = FileUploaderService.UploadFile(image);
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }
                

                await productsService.InsertProduct(newProductViewModel);
                var newProdId = await productsService.GetLastAddedProductId();

                await imagesRepository.AddImage(new ProductImage { PhotoUrl = fileName, ProductId = newProdId });
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoriesService.GetCategories().Result, "Id", "CategoryName");
            ViewData["UserId"] = userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            return View(newProductViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", product.UserId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductPrice,ProductDescription,UserId,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", product.UserId);
            return View(product);
        }

        // GET: Products/UserId/Delete/ProductId
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {

                
            var product = await productsService.GetProductByProductID(Id);

            if (product == null)
            {
                return NotFound();
            }
            await productsService.DeleteProduct(product.Id);
            return RedirectToAction("Index","Products");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

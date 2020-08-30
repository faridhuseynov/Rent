using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ServiceLayers;
using Rent.ViewModels.CategoryViewModels;

namespace Rent.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        IProductsService ps;
        ICategoriesService cs;
        IProposalsService propsService;
        private readonly UserManager<User> userManager;
        private readonly IProposalTypesRepository proposalTypesRepository;
        private readonly IWishListProdsRepository wishListProdsRepository;
        private readonly IRatingsRepository ratingsRepository;
        IEnumerable<Category> categories;
        IEnumerable<ProposalType> proposalTypes;
        public HomeController(IProductsService productsService, ICategoriesService categoriesService,
                    IProposalsService proposalsService, UserManager<User> userManager, IProposalTypesRepository proposalTypesRepository,
                    IWishListProdsRepository wishListProdsRepository, IRatingsRepository ratingsRepository)
        {
            ps = productsService;
            cs = categoriesService;
            propsService = proposalsService;
            this.userManager = userManager;
            this.proposalTypesRepository = proposalTypesRepository;
            this.wishListProdsRepository = wishListProdsRepository;
            this.ratingsRepository = ratingsRepository;
            categories = new List<Category>();
            categories = cs.GetCategories().Result;
            proposalTypes = new List<ProposalType>();
            proposalTypes = proposalTypesRepository.GetProposalTypes().Result;
            //SearchBarViewCompService.GetCategories(HttpContext.Session, categories.ToList());
            //productParams = new ProductParamsForFilter();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            var proposals = propsService.GetProposals();
            var products = ps.GetProducts();
            ViewBag.Registered = users.Count;
            ViewBag.Proposals = proposals.Count();
            ViewBag.Closed = proposals.Where(p => p.ProposalStatus.StatusName == "Closed").Count();
            ViewBag.Products = products.Count();
            ViewBag.Successful = proposals.Where(p => p.ProposalStatus.StatusName == "Closed").ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            //here should be included all users except admin himself
            var users = userManager.Users.ToList();            
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> BlockAndUnblockProduct(string userName, int productId)
        {
            var product =await ps.GetProductByProductID(productId);
            if (product!=null && userName=="admin@rent.com")
            {
                product.Blocked = !(product.Blocked);
                await ps.UpdateProductDetails(product);
            }
            return RedirectToAction("Profile", "Account", new {area="", userName = userName });
        }

        [HttpGet]
        public async Task<IActionResult> ActiveProducts()
        {
            ViewBag.Categories = cs.GetCategories().Result;
            var products = ps.GetProducts().Where(p => p.Blocked == false) ;
            return View(products);
        }


        [HttpGet]
        public async Task<IActionResult> BlockedProducts()
        {
            ViewBag.Categories = cs.GetCategories().Result;
            var products = ps.GetProducts().Where(p=>p.Blocked==true);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            return View(await cs.GetCategories());
        }

        [HttpPost]
        public async Task AddCategory(string categoryName)
        {
            await cs.AddCategory(new NewCategoryViewModel { CategoryName = categoryName });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ServiceLayers;
using Rent.ViewModels.ProductViewModels;
using Rent.ViewModels.ProposalViewModels;

namespace Rent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IProductsService ps;
        ICategoriesService cs;
        IProposalsService propsService;
        private readonly UserManager<User> userManager;
        private readonly IProposalTypesRepository proposalTypesRepository;
        private readonly IWishListProdsRepository wishListProdsRepository;
        IEnumerable<Category> categories;
        IEnumerable<ProposalType> proposalTypes;
        int NoOfRecordsPerPage = 10;
        int NoOfPages;
        int NoOfRecordsToSkip;
        //ProductParamsForFilter productParams;

        public HomeController(ILogger<HomeController> logger, IProductsService productsService, ICategoriesService categoriesService,
            IProposalsService proposalsService, UserManager<User> userManager, IProposalTypesRepository proposalTypesRepository,
            IWishListProdsRepository wishListProdsRepository)
        {
            _logger = logger;
            ps = productsService;
            cs = categoriesService;
            propsService = proposalsService;
            this.userManager = userManager;
            this.proposalTypesRepository = proposalTypesRepository;
            this.wishListProdsRepository = wishListProdsRepository;
            categories = new List<Category>();
            categories = cs.GetCategories().Result;
            proposalTypes = new List<ProposalType>();
            proposalTypes = proposalTypesRepository.GetProposalTypes().Result;
            //SearchBarViewCompService.GetCategories(HttpContext.Session, categories.ToList());
            //productParams = new ProductParamsForFilter();
        }

        public IActionResult Index(int Id = 0, int PageNo = 1, string searchString="")
        {
            IEnumerable<ProductDetailsViewModel> products = new List<ProductDetailsViewModel>();
            NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.Categories = categories;
            if (Id != 0)
                products = ps.GetProducts().Where(x => x.CategoryId == Id);
            else
                products = ps.GetProducts();
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString)
                || p.ProductDescription.Contains(searchString)|| p.Category.CategoryName.Contains(searchString));
            }
            NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count() / Convert.ToDouble(NoOfRecordsPerPage))));
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage);
            //products = ps.GetProducts().Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage);
            ViewBag.CategoryId = Id;
            //if (User.Identity.IsAuthenticated)
            //{

            //    var userFromRepo = userManager.FindByNameAsync(User.Identity.Name);
            //    var wishList = wishListProdsRepository.GetWishListProducts().Result;
            //    ViewBag.WishCount = wishList.ToList().
            //    Where(w => w.UserId == userFromRepo.Result.Id).Count();
            //}
            return View(products);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var user = User.Identity.Name;
            string UserId = "";
            if (user != null)
            {
                var userFromRepo = await userManager.FindByNameAsync(user);
                if (userFromRepo!=null)
                    UserId = userFromRepo.Id;
            }
            var product = await ps.GetProductByProductID(Id);
            if (product != null)
            {
                ViewBag.ProposalTypes = proposalTypes;
                //ViewBag.ProposalTypes = product.p;

                var result = await wishListProdsRepository.GetWishListProducts();
                ViewBag.Wish = result.ToList().FirstOrDefault(
                    w => w.ProductId == Id && w.UserId == UserId);
                return View(product);

            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> SendProposal(int Id, decimal proposedPrice,
            string buyer, int proposalType, DateTime rentStartDate, DateTime rentEndDate)
        {
            var product = ps.GetProductByProductID(Id);
            var _buyer = await userManager.FindByEmailAsync(buyer);
            int newPropId=0;
            if (product != null)
            {
                var newProposal = new NewProposalViewModel();
                if (proposalType==3)
                {
                    newProposal.ProposedRentStartDate = rentStartDate;
                    newProposal.ProposedRentEndDate = rentEndDate;
                }
                newProposal.ProductId = Id;
                newProposal.ProposedPrice = proposedPrice;
                newProposal.OwnerId = product.Result.UserId;
                newProposal.BuyerId = _buyer.Id;
                newProposal.ProposalTypeId = proposalType;
                newProposal.ProposalAdded = DateTime.Now;
                //in the proposalstatus table the id = 1 is related to Open status
                newProposal.ProposalStatusId = 1;
                newPropId = await propsService.InsertProposal(newProposal);
            }
            if (newPropId > 0)
                TempData["Success"] = "Təklif uğurla göndərilmişdir";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task AddProductToWishlist(int productId, string buyer)
        {
            var _buyer = await userManager.FindByNameAsync(buyer);
            if (_buyer != null)
            {
                var wishCheck = wishListProdsRepository.GetWishListProducts().Result.FirstOrDefault(w => w.ProductId == productId && w.UserId == _buyer.Id);
                if (wishCheck == null)
                    await wishListProdsRepository.InsertProduct(new WishListProduct { ProductId = productId, UserId = _buyer.Id });
            }
            
            //ViewBag.WishCount = wishListProdsRepository.GetWishListProducts().Result.Where(u => u.UserId == userId).Count();
        }
        [HttpPost]
        public async Task RemoveProductFromWishlist(int productId, string buyer)
        {
            var _buyer = await userManager.FindByNameAsync(buyer);
            if (_buyer != null)
            {
                var wishCheck = wishListProdsRepository.GetWishListProducts().Result.FirstOrDefault(w => w.ProductId == productId && w.UserId == _buyer.Id);
                if (wishCheck != null)
                    await wishListProdsRepository.DeleteProduct(wishCheck);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

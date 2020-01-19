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
        IEnumerable<Category> categories;
        IEnumerable<ProposalType> proposalTypes;
        int NoOfRecordsPerPage = 10;
        int NoOfPages;
        int NoOfRecordsToSkip;
        //ProductParamsForFilter productParams;

        public HomeController(ILogger<HomeController> logger, IProductsService productsService, ICategoriesService categoriesService,
            IProposalsService proposalsService, UserManager<User> userManager, IProposalTypesRepository proposalTypesRepository)
        {
            _logger = logger;
            ps = productsService;
            cs = categoriesService;
            propsService = proposalsService;
            this.userManager = userManager;
            this.proposalTypesRepository = proposalTypesRepository;
            categories = new List<Category>();
            categories = cs.GetCategories().Result;
            proposalTypes = new List<ProposalType>();
            proposalTypes = proposalTypesRepository.GetProposalTypes().Result;
            //productParams = new ProductParamsForFilter();
        }

        public IActionResult Index(int Id = 0, int PageNo = 1)
        {
            IEnumerable<ProductDetailsViewModel> products = new List<ProductDetailsViewModel>();
            NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.Categories = categories;
            if (Id != 0)
                products = ps.GetProducts().Where(x => x.CategoryId == Id);
            else
                products = ps.GetProducts();
            NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count() / Convert.ToDouble(NoOfRecordsPerPage))));
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage);
            //products = ps.GetProducts().Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage);
            ViewBag.CategoryId = Id;

            return View(products);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var product = await ps.GetProductByProductID(Id);
            if (product != null)
            {
                ViewBag.ProposalTypes = proposalTypes;
                return View(product);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> SendProposal(int Id, decimal proposedPrice,string buyer, int proposalType)
        {
            var product = ps.GetProductByProductID(Id);
            var _buyer = await userManager.FindByEmailAsync(buyer);
            int newPropId=0;
            if (product != null)
            {
                newPropId = await propsService.InsertProposal(new NewProposalViewModel
                {
                    ProductId = Id,
                    ProposedPrice = proposedPrice,
                    OwnerId = product.Result.UserId,
                    BuyerId = _buyer.Id,
                    ProposalTypeId = proposalType                    
                });
            }
            if (newPropId > 0)
                TempData["Success"] = "Proposal successfully sent";
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

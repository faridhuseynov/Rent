using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ServiceLayers;

namespace Rent.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            //_logger = logger;
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
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            var proposals = propsService.GetProposals();
            ViewBag.Registered = users.Count;
            ViewBag.Proposals = proposals.Count();
            ViewBag.Closed = proposals.Where(p => p.ProposalStatus.StatusName == "Closed").Count();
            ViewBag.Rejected = proposals.Where(p => p.ProposalStatus.StatusName == "Rejected").Count();
            ViewBag.Successful = proposals.Where(p => p.ProposalStatus.StatusName == "Closed").ToList();
            return View();
        }
    }
}
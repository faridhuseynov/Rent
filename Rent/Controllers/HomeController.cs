using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ServiceLayers;
using Rent.ViewModels.ProductViewModels;

namespace Rent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IProductsService ps;
        ICategoriesService cs;
        IProposalsRepository props;
        IEnumerable<Category> categories;
        int NoOfRecordsPerPage =10;
        int NoOfPages;
        int NoOfRecordsToSkip;
        //ProductParamsForFilter productParams;

        public HomeController(ILogger<HomeController> logger, IProductsService productsService, ICategoriesService categoriesService,
            IProposalsRepository proposalsRepository)
        {
            _logger = logger;
            ps = productsService;
            cs = categoriesService;
            props = proposalsRepository;
            categories = new List<Category> ();
            categories = cs.GetCategories().Result;
            //productParams = new ProductParamsForFilter();
        }

        public IActionResult Index(int Id=0, int PageNo = 1)
        {
            IEnumerable<ProductDetailsViewModel> products=new List<ProductDetailsViewModel>();
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

        public IActionResult Details(int Id)
        {
            var product = ps.GetProductByProductID(Id);
            if (product!=null)
            {
                product = ps.GetProductByProductID(Id);
                return View(product);
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult SendProposal(int Id, decimal proposedPrice)
        {
            var product = ps.GetProductByProductID(Id);
            if (product != null)
            {
                props.AddProposal(new Proposal
                {
                    ProductId = Id,
                    ProposedPrice = proposedPrice,
                    OwnerId = product.UserId,
                    BuyerId = "6ee849df-68ee-4f64-ba84-b417c0979d51"
                });
                TempData["Success"] = $"Proposal {props.GetLatestProposalID()} successfully added!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

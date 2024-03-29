﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ServiceLayers;
using Rent.ViewModels.ProductViewModels;
using Rent.ViewModels.ProposalTypeViewModels;
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
        private readonly IProposalTypesService proposalTypesService;
        private readonly IWishListProdsRepository wishListProdsRepository;
        private readonly IRatingsRepository ratingsRepository;
        private readonly IHtmlLocalizer<HomeController> localizer;
        IEnumerable<Category> categories;
        IEnumerable<ProposalTypeDetailsViewModel> proposalTypes;
        int NoOfRecordsPerPage = 6;
        int NoOfPages;
        int NoOfRecordsToSkip;
        //ProductParamsForFilter productParams;

        public HomeController(ILogger<HomeController> logger, IProductsService productsService, ICategoriesService categoriesService,
            IProposalsService proposalsService, UserManager<User> userManager, IProposalTypesService proposalTypesService,
            IWishListProdsRepository wishListProdsRepository, IRatingsRepository ratingsRepository, IHtmlLocalizer<HomeController> localizer)
        {
            _logger = logger;
            ps = productsService;
            cs = categoriesService;
            propsService = proposalsService;
            this.userManager = userManager;
            this.proposalTypesService = proposalTypesService;
            this.wishListProdsRepository = wishListProdsRepository;
            this.ratingsRepository = ratingsRepository;
            this.localizer = localizer;
            categories = new List<Category>();
            categories = cs.GetCategories().Result;
            proposalTypes = proposalTypesService.GetProposalTypes().Result;
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(1) });
            return Redirect(returnUrl);
        }

        public IActionResult Index(string Id = "All", int PageNo = 1, string searchString="")
        {
            IEnumerable<ProductDetailsViewModel> products = new List<ProductDetailsViewModel>();
            NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.Categories = categories;
            if (Id != "All")
                products = ps.GetProducts().Where(p => p.SubcategoryId.ToString() == Id && p.Blocked!=true);
            else
                products = ps.GetProducts().Where(p=>p.Blocked!=true);
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToUpper();
                var newSearchString = searchString[0] + searchString.Substring(1).ToLower();
                products = (products.Where(p => p.ProductName.Contains(newSearchString)).Concat(products.Where(p => p.ProductDescription.Contains(newSearchString)))
                    .Concat(products.Where(p=>p.Subcategory.SubcategoryName.Contains(newSearchString)))).Distinct();
               
            }
            if (User.Identity.IsAuthenticated==true)
            {
                //products = products.Where(o => o.User.UserName != User.Identity.Name);
                //ViewBag.MyProducts = products.Where(o => o.User.UserName == User.Identity.Name);
            }
            NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count() / Convert.ToDouble(NoOfRecordsPerPage))));
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage);

            ViewBag.CategoryId = Id;

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
            if (product != null && product.Blocked!=true)
            {
                ViewBag.ProposalTypes = proposalTypes;

                var result = await wishListProdsRepository.GetAllWishListProducts();
                ViewBag.Wish = result.ToList().FirstOrDefault(
                    w => w.ProductId == Id && w.UserId == UserId);
                return View(product);
                
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> SendProposal(int Id, decimal proposedPrice,string proposalMessage,
            string buyer, int proposalType, DateTime rentStartDate, DateTime rentEndDate, int proposedAmount)
        {
            var product = await ps.GetProductByProductID(Id);
            var propType = await proposalTypesService.GetProposalType(proposalType);
            var _buyer = await userManager.FindByNameAsync(buyer);
            int newPropId=0;
            if (product != null && product.Blocked!=true)
            {
                var newProposal = new NewProposalViewModel();
                newProposal.ProposedPrice = proposedPrice;
                if (propType.Type.ToLower() == "rent")
                {
                    // doesn't work properly, date is being sent without correction
                    newProposal.ProposedRentStartDate = rentStartDate;
                    newProposal.ProposedRentEndDate = rentEndDate.AddDays(1);
                    if (newProposal.ProposedRentStartDate.Year.ToString() == "1")
                    {
                        newProposal.ProposedRentStartDate = DateTime.Now;
                        newProposal.ProposedRentStartDate = newProposal.ProposedRentStartDate.AddDays(1);
                    }
                    if (newProposal.ProposedRentEndDate.Year.ToString() == "1")
                    {
                        newProposal.ProposedRentEndDate = newProposal.ProposedRentStartDate;
                        newProposal.ProposedRentEndDate = newProposal.ProposedRentEndDate.AddDays(product.MinLendDays);
                    }
                    newProposal.ProposedTotalPrice = newProposal.ProposedPrice *
                        Int64.Parse(TimeSpan.FromTicks(rentEndDate.AddDays(1).Ticks - rentStartDate.Ticks).TotalDays.ToString());
                }
                else
                    newProposal.ProposedTotalPrice = newProposal.ProposedPrice;
                newProposal.ProductId = Id;
                newProposal.OwnerId = product.UserId;
                newProposal.BuyerId = _buyer.Id;
                newProposal.ProposalTypeId = proposalType;
                newProposal.ProposalAdded = DateTime.Now;
                //in the proposalstatus table the id = 1 is related to Open status
                newProposal.ProposalStatusId = 1;
                newProposal.ProposalMessage = proposalMessage;
                newProposal.ProposedAmount = proposedAmount;
                newPropId = await propsService.InsertProposal(newProposal);
            }
            if (newPropId > 0)
            {
                var result = localizer["SuccessfullProposalMessage"].Value;
                TempData["Success"] =result;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task AddProductToWishlist(int productId, string buyer)
        {
            var _buyer = await userManager.FindByNameAsync(buyer);
            if (_buyer != null)
            {
                var wishCheck =  wishListProdsRepository.GetAllWishListProducts().Result.FirstOrDefault(w => w.ProductId == productId && w.UserId == _buyer.Id);
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
                var wishCheck = wishListProdsRepository.GetAllWishListProducts().Result.FirstOrDefault(w => w.ProductId == productId && w.UserId == _buyer.Id);
                if (wishCheck != null)
                    await wishListProdsRepository.DeleteProduct(wishCheck);
            }
        }

        [HttpPost]
        public async Task AddNewRating(int productId, string rater, string review, int score)
        {
            var _rater = await userManager.FindByNameAsync(rater);            
            if (_rater!=null)
            {
                var newReview = new Rate();
                newReview.Note = score;
                newReview.Review = review;
                newReview.UserId = _rater.Id;
                newReview.ProductId = productId;
                var date = new DateTime();
                date = DateTime.UtcNow;
                newReview.DateAdded = date;
                await ratingsRepository.AddRating(newReview);
                await ps.UpdateRating(productId);
            }

        }

        public async Task<IActionResult> Wishlist(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var products = user.WishList.ToList();
                return View(products);
            }

            return BadRequest();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

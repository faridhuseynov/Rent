﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using Rent.ServiceLayers;
using Rent.ViewModels.ProposalViewModels;

namespace Rent.Controllers
{
    public class ProposalsController : Controller
    {
        private readonly IProposalsService proposalsService;
        private readonly UserManager<User> userManager;
        private readonly IProductsService productsService;

        public ProposalsController(IProposalsService proposalsService, UserManager<User> userManager,
            IProductsService productsService)
        {
            this.proposalsService = proposalsService;
            this.userManager = userManager;
            this.productsService = productsService;
        }

        // GET: IncomingProposals
        public async Task<IActionResult> IncomingProposals(string? userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            //if (user!=null)
            //{
            //    var proposals = proposalsService.GetProposalsByUserId(user.Id).Where(u => u.OwnerId == user.Id)
            //        .OrderByDescending(p => p.ProposalAdded).OrderByDescending(p => p.ProposalClosed).OrderBy(p => p.ProposalStatusId);
            //    return View(proposals);
            //}
            if (user != null)
            {
                ViewBag.UserId = user.Id;
                return View();
            }
            return RedirectToAction("Home", "Index");
        }

        // GET: OutgoingProposals
        public async Task<IActionResult> OutgoingProposals(string? userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                //var proposals = proposalsService.GetProposalsByUserId(user.Id).Where(u => u.BuyerId == user.Id)
                //    .OrderByDescending(p => p.ProposalAdded).OrderByDescending(p => p.ProposalClosed).OrderBy(p => p.ProposalStatusId);
                //return View(proposals);
                ViewBag.UserId = user.Id;
                return View();

            }
            return RedirectToAction("Home", "Index");
        }

        [HttpGet]
        public PartialViewResult _FilteredInboxProposals(string userId, string selection)
        {
            var proposals = proposalsService.GetProposalsByUserId(userId).Where(u=>u.OwnerId==userId);
                if (selection!="All")
                {
                    proposals = proposals.Where(p => p.ProposalStatus.StatusName == selection);
                }
            return PartialView(proposals.OrderByDescending(p => p.ProposalAdded));
        }


        [HttpGet]
        public PartialViewResult _FilteredOutboxProposals(string userId, string selection)
        {
            var proposals = proposalsService.GetProposalsByUserId(userId).Where(u => u.BuyerId == userId);

            if (selection != "All")
            {
                proposals = proposals.Where(p => p.ProposalStatus.StatusName == selection);
            }
            return PartialView(proposals.OrderByDescending(p => p.ProposalAdded));
        }
        // GET: Proposals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await proposalsService.GetProposalByProposalId((int)id);
            if (proposal == null)
            {
                return NotFound();
            }

            EditProposalDetailsViewModel proposalToEdit = new EditProposalDetailsViewModel()
            {
                ProposedPrice = proposal.ProposedPrice,
                ProposedRentEndDate = proposal.ProposedRentEndDate,
                ProposedRentStartDate = proposal.ProposedRentStartDate,
                ProposalMessage = proposal.ProposalMessage,
                ProposalType=proposal.ProposalType,
                ProposalTypeId=proposal.ProposalTypeId,
                Product =proposal.Product,
                ProductId = proposal.ProductId,
                ProposedAmount=proposal.ProposedAmount
                
            };
            return View(proposalToEdit);
        }

        // POST: Proposals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProposalDetailsViewModel proposalToEdit)
        {
            if (id != proposalToEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await proposalsService.UpdateProposalDetails(proposalToEdit);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProposalExists(proposalToEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("OutgoingProposals", new { userName = User.Identity.Name});
            }
            return View(proposalToEdit);
        }

        // GET: Proposals/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await proposalsService.DeleteProposal(id);
            return RedirectToAction("OutgoingProposals", new { userName = User.Identity.Name });
        }
        public async Task<IActionResult> AcceptOrRejectProposal(int proposalId, int statusId, int productId)
        {
            var responseDate = new DateTime();
            responseDate = DateTime.Now;
            var proposal = await proposalsService.GetProposalByProposalId(proposalId);
            //if the proposal is accepted the product.availableAmount should also be decreased
            if (statusId==2)
            {
               var product = await productsService.GetProductToUpdate(productId);
                proposal.Product.CurrentlyRented = proposal.Product.CurrentlyRented + proposal.ProposedAmount;
                await productsService.UpdateProductDetails(product);

            }

            await proposalsService.AcceptOrRejectProposal(proposalId, statusId, responseDate);
            return RedirectToAction("IncomingProposals", new { userName = User.Identity.Name });
        }

        private bool ProposalExists(int id)
        {
            return proposalsService.GetProposals().Any(p => p.Id == id);
        }
    }
}

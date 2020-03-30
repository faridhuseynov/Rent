using System;
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
        private readonly AppDbContext _context;
        private readonly IProposalsService proposalsService;
        private readonly UserManager<User> userManager;

        public ProposalsController(AppDbContext context,IProposalsService proposalsService, UserManager<User> userManager)
        {
            _context = context;
            this.proposalsService = proposalsService;
            this.userManager = userManager;
        }

        // GET: Proposals
        public async Task<IActionResult> IncomingProposals(string? userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user!=null)
            {
                var proposals = proposalsService.GetProposalsByUserId(user.Id).Where(u => u.OwnerId == user.Id)
                    .OrderByDescending(p => p.ProposalAdded).OrderByDescending(p => p.ProposalClosed).OrderBy(p => p.ProposalStatusId);
                    //.OrderByDescending(p => p.ProposalAdded).OrderByDescending(p => p.ProposalClosed).OrderBy(p => p.ProposalStatusId);
                return View(proposals);

            }
            return RedirectToAction("Home", "Index");
            //var appDbContext = _context.Proposals.Include(p => p.Buyer).Include(p => p.Owner).Include(p => p.Product);

            //return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> OutgoingProposals(string? userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var proposals = proposalsService.GetProposalsByUserId(user.Id).Where(u => u.BuyerId == user.Id)
                    .OrderByDescending(p => p.ProposalAdded).OrderByDescending(p => p.ProposalClosed).OrderBy(p => p.ProposalStatusId);
                return View(proposals);

            }
            return RedirectToAction("Home", "Index");
            //var appDbContext = _context.Proposals.Include(p => p.Buyer).Include(p => p.Owner).Include(p => p.Product);

            //return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> AcceptOrRejectProposal(int proposalId, int statusId)
        {
            var responseDate = new DateTime();
            responseDate = DateTime.Now;
            await proposalsService.AcceptOrRejectProposal(proposalId, statusId, responseDate);
            return RedirectToAction("IncomingProposals", new { userName = User.Identity.Name });
        }
        // GET: Proposals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proposal = await _context.Proposals
                .Include(p => p.Buyer)
                .Include(p => p.Owner)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proposal == null)
            {
                return NotFound();
            }

            return View(proposal);
        }

        // GET: Proposals/Create
        public IActionResult Create()
        {
            ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Proposals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,ProposedPrice,OwnerId,BuyerId,ProposalStatus")] Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", proposal.BuyerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", proposal.OwnerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", proposal.ProductId);
            return View(proposal);
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
                ProductId = proposal.ProductId
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

        private bool ProposalExists(int id)
        {
            return proposalsService.GetProposals().Any(p => p.Id == id);
        }
    }
}

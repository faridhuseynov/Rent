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
            var user = userManager.FindByNameAsync(userName).Result;
            if (user!=null)
            {
                var proposals = proposalsService.GetProposalsByUserId(user.Id).Where(u=>u.OwnerId==user.Id);
                return View(proposals);

            }
            return RedirectToAction("Home", "Index");
            //var appDbContext = _context.Proposals.Include(p => p.Buyer).Include(p => p.Owner).Include(p => p.Product);

            //return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> OutgoingProposals(string? userName)
        {
            var user = userManager.FindByNameAsync(userName).Result;
            if (user != null)
            {
                var proposals = proposalsService.GetProposalsByUserId(user.Id).Where(u => u.BuyerId == user.Id);
                return View(proposals);

            }
            return RedirectToAction("Home", "Index");
            //var appDbContext = _context.Proposals.Include(p => p.Buyer).Include(p => p.Owner).Include(p => p.Product);

            //return View(await appDbContext.ToListAsync());
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

            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }
            ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", proposal.BuyerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", proposal.OwnerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", proposal.ProductId);
            return View(proposal);
        }

        // POST: Proposals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,ProposedPrice,OwnerId,BuyerId,ProposalStatus")] Proposal proposal)
        {
            if (id != proposal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proposal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProposalExists(proposal.Id))
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
            ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", proposal.BuyerId);
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", proposal.OwnerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", proposal.ProductId);
            return View(proposal);
        }

        // GET: Proposals/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Proposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            _context.Proposals.Remove(proposal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProposalExists(int id)
        {
            return _context.Proposals.Any(e => e.Id == id);
        }
    }
}

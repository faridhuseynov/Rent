using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface IProposalsRepository
    {
        Task<int> AddProposal(Proposal proposal);
        void UpdateProposalDetails(Proposal proposal);
        //void UpdateUserPassword(User user);
        void DeleteProposal(int proposalId);
        Task<IEnumerable<Proposal>> GetProposals();
        Task<IEnumerable<Proposal>> GetProposalsByUserId(string UserId);

        Task<Proposal> GetProposalByProposalID(int proposalId);
        Task<int> GetLatestProposalID();
        Task AcceptOrRejectProposal(int proposalId, int statusId);
    }
    public class ProposalsRepository:IProposalsRepository
    {
        private readonly AppDbContext db;
        public ProposalsRepository(AppDbContext dbContext)
        {
            db = dbContext;
        }
        async public Task<int> AddProposal(Proposal proposal)
        {
            await db.Proposals.AddAsync(proposal);
            await db.SaveChangesAsync();
            return proposal.Id;
        }

        async public void UpdateProposalDetails(Proposal proposal)
        {
            var checkProposal = await db.Proposals.FirstOrDefaultAsync(p => p.Id == proposal.Id);
            if (checkProposal!=null)
            {
                checkProposal.ProposalStatus = proposal.ProposalStatus;
                checkProposal.ProposedPrice = proposal.ProposedPrice;
                await db.SaveChangesAsync();
            }
        }

        async public void DeleteProposal(int proposalId)
        {
            var proposal = await db.Proposals.FirstOrDefaultAsync(p => p.Id == proposalId);
            if (proposal != null)
            {
                db.Proposals.Remove(proposal);
                await db.SaveChangesAsync();
            }
        }

        async public Task<IEnumerable<Proposal>> GetProposals()
        {
            //return await db.Proposals.Include(p=>p.Product).Include(b=>b.Buyer).Include(o=>o.Owner).ToListAsync();
            return await db.Proposals.ToListAsync();
        }

        async public Task<IEnumerable<Proposal>> GetProposalsByUserId(string UserId)
        {
            return await db.Proposals.Where(x => x.OwnerId == UserId || x.BuyerId == UserId).ToListAsync();
        }
        async public Task<Proposal> GetProposalByProposalID(int proposalId)
        {
            var proposal = await db.Proposals.FirstOrDefaultAsync(p => p.Id == proposalId);
            if (proposal!=null)
                return proposal;
            return null;
        }
        async public Task<int> GetLatestProposalID()
        {
            var lastProposal = await db.Proposals.LastOrDefaultAsync();
            if (lastProposal != null)
                return lastProposal.Id;
            return 0;
        }

        public async Task AcceptOrRejectProposal(int proposalId, int statusId)
        {
            var proposal = await db.Proposals.FirstOrDefaultAsync(p => p.Id == proposalId);
            if (proposal!=null)
            {
                proposal.ProposalStatusId = statusId;
                db.Proposals.Update(proposal);
                await db.SaveChangesAsync();
            }
        }
    }
}

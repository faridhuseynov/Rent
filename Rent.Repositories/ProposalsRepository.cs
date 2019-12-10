using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface IProposalsRepository
    {
        void AddProposal(Proposal proposal);
        void UpdateProposalDetails(Proposal proposal);
        //void UpdateUserPassword(User user);
        void DeleteProposal(int proposalId);
        Task<IEnumerable<Proposal>> GetProposals();
        Task<Proposal> GetProposalByProposalID(int proposalId);
        Task<int> GetLatestProposalID();
    }
    public class ProposalsRepository:IProposalsRepository
    {
        private readonly AppDbContext db;
        public ProposalsRepository(AppDbContext dbContext)
        {
            db = dbContext;
        }
        async public void AddProposal(Proposal proposal)
        {
            await db.Proposals.AddAsync(proposal);
            await db.SaveChangesAsync();
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

        async public Task<IEnumerable<Proposal>> GetProposals() {
            return await db.Proposals.ToListAsync();
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
    }
}

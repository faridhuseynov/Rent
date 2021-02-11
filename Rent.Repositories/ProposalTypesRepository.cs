using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface IProposalTypesRepository
    {
        Task<IEnumerable<ProposalType>> GetProposalTypes();
        Task<ProposalType> GetProposalTypeAsync(int proposalTypeId);
    }
    public class ProposalTypesRepository : IProposalTypesRepository
    {
        private readonly AppDbContext db;
        public ProposalTypesRepository(AppDbContext dbContext)
        {
            db = dbContext;
        }
        public async Task<IEnumerable<ProposalType>> GetProposalTypes()
        {
            var proposalTypes = await db.ProposalTypes.ToListAsync();
            return proposalTypes;
        }

        public async Task<ProposalType> GetProposalTypeAsync(int proposalTypeId)
        {
            var proposalType = await db.ProposalTypes
                                    .FirstOrDefaultAsync(pt => pt.Id == proposalTypeId);
            return proposalType;
        }
    }
}

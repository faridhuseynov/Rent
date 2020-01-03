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
    }
}

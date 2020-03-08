using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{

    public interface IRatingsRepository
    {
        Task<int> AddRating(Rate rate);
        Task RemoveRating(int ratingId);
    }
    public class RatingsRepository:IRatingsRepository
    {
        private readonly AppDbContext dbContext;

        public RatingsRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> AddRating(Rate rate)
        {
            await dbContext.Rates.AddAsync(rate);
            await dbContext.SaveChangesAsync();
            return rate.Id;
        }

        public async Task RemoveRating(int ratingId)
        {
            var rate = await dbContext.Rates.FirstOrDefaultAsync(r => r.Id == ratingId);
            if (rate!=null)
            {
                dbContext.Rates.Remove(rate);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

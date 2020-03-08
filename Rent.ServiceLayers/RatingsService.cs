using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers
{
    public interface IRatingsService
    {
        Task<int> AddRating(Rate rate);
        Task RemoveRating(int ratingId);
    }
    public class RatingsService:IRatingsService
    {
        public RatingsService()
        {

        }

        public Task<int> AddRating(Rate rate)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRating(int ratingId)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers.Hubs
{
    public class FavoriteCountServiceHub:Hub
    {
        public async Task IncreaseFavoriteCount(int currentCount)
        {
            await Clients.Caller.SendAsync("ReceiveUpdatedCount", currentCount + 1);
        }

        public async Task ReduceFavoriteCount(int currentCount)
        {

            await Clients.Caller.SendAsync("ReceiveUpdatedCount", currentCount - 1);
        }
    }
}

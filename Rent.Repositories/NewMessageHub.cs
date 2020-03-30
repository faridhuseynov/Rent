using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public class HubService : Hub
    {
        public async Task GetMessageCount(string userId)
        {
            await Clients.All.SendAsync("GetMessagesCount", userId);
        }
    }
}

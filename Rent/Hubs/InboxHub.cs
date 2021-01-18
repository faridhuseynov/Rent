using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rent.Hubs
{
    class InboxHub : Hub
    {
        public async Task SendMessage(string recipient, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", recipient, message);
        }
    }
}

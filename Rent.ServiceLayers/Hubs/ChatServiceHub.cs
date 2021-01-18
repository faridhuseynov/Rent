using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers.Hubs
{
    public class ChatServiceHub:Hub
    {
        public async Task SendMessage(string recipient, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", recipient, message);
        }
    }
}

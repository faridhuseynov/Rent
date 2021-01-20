using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers.Hubs
{
    public class ChatServiceHub:Hub
    {
        public async Task SendPrivateMessage(string sender, string recipient, string message)
        {
            //to be changed for only 2 people, currently sends to everybody
            await Clients.All.SendAsync("ReceivePrivateMessage", recipient, sender, message);
        }


        public async Task SendTypingNotification(string sender, string recipient)
        {
            await Clients.All.SendAsync("ReceiveTypingNotification", sender, recipient);
        }

        //public async Task SendTypingNotification(string sender, string recipient)
        //{
        //    await Clients.User(recipient).SendAsync("ReceiveTypingNotification", sender, recipient);
        //}
    }
}

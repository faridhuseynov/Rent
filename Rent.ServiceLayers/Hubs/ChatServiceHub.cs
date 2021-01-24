﻿using Microsoft.AspNetCore.SignalR;
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
            await Clients.Client(recipient).SendAsync("ReceivePrivateMessage", message);
        }

        public async Task SendTypingNotification(string sender, string recipient)
        {
            await Clients.All.SendAsync("ReceiveTypingNotification", sender, recipient);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        //public async Task SendTypingNotification(string sender, string recipient)
        //{
        //    await Clients.User(recipient).SendAsync("ReceiveTypingNotification", sender, recipient);
        //}
    }
}

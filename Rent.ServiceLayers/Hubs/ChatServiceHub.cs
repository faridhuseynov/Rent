﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers.Hubs
{
    public class ChatServiceHub:Hub
    {
        public async Task SendPrivateMessage(string recipient, string message)
        {
            //to be changed for only 2 people, currently sends to everybody
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
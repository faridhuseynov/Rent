using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers.Hubs
{
    public class ChatServiceHub:Hub
    {
        private readonly IDictionary<string, IList<string>> userConnectionIdSet;

        public ChatServiceHub(IDictionary<string, IList<string>> userConnectionIdSet)
        {
            this.userConnectionIdSet = userConnectionIdSet;
        }
        public async Task SendPrivateMessage(string recipientConnectionId, string message)
        {
            //to be changed for only 2 people, currently sends to everybody
            await Clients.Client(recipientConnectionId)
                .SendAsync("ReceivePrivateMessage", message);
            //await Clients.All.SendAsync("ReceivePrivateMessage",message);
        }



        //public async Task SendTypingNotification(string sender, string recipient)
        //{
        //    await Clients.All.SendAsync("ReceiveTypingNotification", sender, recipient);
        //}

        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.Identity.Name;
            if (!userConnectionIdSet.ContainsKey(userName))
            {
                userConnectionIdSet.Add(userName, new List<string>());
            }
            userConnectionIdSet[userName].Add(Context.ConnectionId);
            await Clients.Caller.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //var userName = Context.User.Identity.Name;
            //var connectionId = Context.ConnectionId;
            //userConnectionIdSet[userName].Remove(connectionId);
            //await Clients.Caller.SendAsync("UserDisconnected", Context.ConnectionId);
            //await base.OnDisconnectedAsync(exception);
        }

        //public async Task SendTypingNotification(string sender, string recipient)
        //{
        //    await Clients.User(recipient).SendAsync("ReceiveTypingNotification", sender, recipient);
        //}
    }
}

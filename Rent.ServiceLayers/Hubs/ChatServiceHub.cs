using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Rent.DomainModels.Models;
using Rent.DomainModels.Models.HubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatServiceHub : Hub
    {
        private UserInfoInMemory _userInfoInMemory;
        public ChatServiceHub(UserInfoInMemory userInfoInMemory)
        {
            _userInfoInMemory = userInfoInMemory;
        }

        public async Task Leave()
        {
            _userInfoInMemory.Remove(Context.User.Identity.Name);
            await Clients.AllExcept(new List<string> { Context.ConnectionId }).SendAsync(
                "UserLeft",
                Context.User.Identity.Name
                );
        }

        public async Task Join()
        {
            if (!_userInfoInMemory.AddUpdate(Context.User.Identity.Name, Context.ConnectionId))
            {
                // new user 
                var list = _userInfoInMemory.GetAllUsersExceptThis(Context.User.Identity.Name).ToList();
                await Clients.AllExcept(new List<string> { Context.ConnectionId }).SendAsync(
                    "NewOnlineUser",
                    _userInfoInMemory.GetUserInfo(Context.User.Identity.Name)
                    );
            }
            else
            {
                // existing user joined again
            }

            await Clients.Client(Context.ConnectionId).SendAsync(
                "Joined",
                _userInfoInMemory.GetUserInfo(Context.User.Identity.Name
                ));

            await Clients.Client(Context.ConnectionId).SendAsync(
                "OnlineUsers",
                _userInfoInMemory.GetAllUsersExceptThis(Context.User.Identity.Name)
                );
        }

        public Task SendDirectMessage(string message, string targetUserName)
        {
            var userInfoSender = _userInfoInMemory.GetUserInfo(Context.User.Identity.Name);
            var userInfoReceiver = _userInfoInMemory.GetUserInfo(targetUserName);
            return Clients.Client(userInfoReceiver.ConnectionId).SendAsync("SendDM", message,userInfoSender);
        }
        //    private readonly IDictionary<string, IList<string>> userConnectionIdSet;

        //    public ChatServiceHub(IDictionary<string, IList<string>> userConnectionIdSet)
        //    {
        //        this.userConnectionIdSet = userConnectionIdSet;
        //    }
        //    public async Task SendPrivateMessage(string recipientConnectionId, string message)
        //    {
        //        //to be changed for only 2 people, currently sends to everybody
        //        await Clients.Client(recipientConnectionId)
        //            .SendAsync("ReceivePrivateMessage", message);
        //        //await Clients.All.SendAsync("ReceivePrivateMessage",message);
        //    }



        //    //public async Task SendTypingNotification(string sender, string recipient)
        //    //{
        //    //    await Clients.All.SendAsync("ReceiveTypingNotification", sender, recipient);
        //    //}

        //    public override async Task OnConnectedAsync()
        //    {
        //        var userName = Context.User.Identity.Name;
        //        if (!userConnectionIdSet.ContainsKey(userName))
        //        {
        //            userConnectionIdSet.Add(userName, new List<string>());
        //        }
        //        userConnectionIdSet[userName].Add(Context.ConnectionId);
        //        await Clients.Caller.SendAsync("UserConnected", Context.ConnectionId);
        //        await base.OnConnectedAsync();
        //    }

        //    public override async Task OnDisconnectedAsync(Exception exception)
        //    {
        //        //var userName = Context.User.Identity.Name;
        //        //var connectionId = Context.ConnectionId;
        //        //userConnectionIdSet[userName].Remove(connectionId);
        //        //await Clients.Caller.SendAsync("UserDisconnected", Context.ConnectionId);
        //        //await base.OnDisconnectedAsync(exception);
        //    }

        //    //public async Task SendTypingNotification(string sender, string recipient)
        //    //{
        //    //    await Clients.User(recipient).SendAsync("ReceiveTypingNotification", sender, recipient);
        //    //}
        //}
    }
}

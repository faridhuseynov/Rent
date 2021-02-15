using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ServiceLayers;
using Rent.ViewModels.MessageViewModel;

namespace Rent.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly UserManager<User> userManager;

        public MessagesController(IMessagesService messagesService, UserManager<User> userManager)
        {
            this.messagesService = messagesService;
            this.userManager = userManager;
        }

        // GET: Messages
        [HttpGet]
        public IActionResult Inbox()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var userFromRepo = await  userManager.FindByNameAsync(User.Identity.Name);
            var messages = await messagesService.GetMessagesForUserByUserId(userFromRepo.Id);
            var sortedMessageList = messages.OrderByDescending(m => m.MessageSent);
            //var response =new Array{ 
            //    caller:User.Identity.Name,
            //    sortedMessageList
            //}
            return Json(sortedMessageList);
        }


        // GET: Messages
        [HttpGet]
        public async Task<IActionResult> Outbox()
        {
            var userFromRepo = userManager.FindByNameAsync(User.Identity.Name).Result;
            var messages = await messagesService.GetMessagesForUserByUserId(userFromRepo.Id);
            return View(messages.OrderByDescending(m=>m.MessageSent));
        }

        //[HttpGet]
        //public async Task<IActionResult> _MessageThread(string recipient, string sender)
        //{
        //    if (recipient!=null && sender!=null)
        //    {
        //        ViewBag.Sender = sender;
        //        ViewBag.Recipient = recipient;
        //        var userFromRepoSender = await userManager.FindByNameAsync(sender);
        //        var userFromRepoRecipient = await userManager.FindByNameAsync(recipient);
        //        //var messages =await messageRepo.GetMessageThread(userFromRepoSender.Id, userFromRepoRecipient.Id);
        //        var messages=[];
        //        return PartialView(messages);
        //    }
        //    return BadRequest();
        //}
        [HttpPost]
        public async Task Create(string recipientUserName, string content)
        {
            var recipient = await userManager.FindByNameAsync(recipientUserName);
            var sender = await userManager.FindByNameAsync(User.Identity.Name);
            var date = new DateTime();
            date = DateTime.UtcNow;
            var newMessage = new NewMessageViewModel();
            if (recipient!=null && sender!=null)
            {
                var message = new NewMessageViewModel
                {
                    RecipientId = recipient.Id, SenderId = sender.Id, Content = content,
                    IsRead = false, MessageSent = date 
                };
                await messagesService.AddNewMessage(message);
            }
        }
       
        [HttpGet]
        public async Task<IActionResult> GetMessagesCount()
        {
            var userName = User.Identity.Name;
            var userId = await userManager.FindByNameAsync(userName);
            //delete the below and uncomment Ok(messageRepo....) --> this is for the SignalR
            return Ok();
            //return Ok(messageRepo.GetUserMessagesCount(userId.Id));
        }

        [HttpPost]
        public async Task<string> AddConnection(string connectionId)
        {
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            user.ConnectionId = connectionId;
            await userManager.UpdateAsync(user);
            return user.ConnectionId;
        }

        [HttpGet]
        public async Task<string> GetConnection(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user!=null)
            {
                if (!String.IsNullOrWhiteSpace(user.ConnectionId))
                {
                    return user.ConnectionId;
                }
            }
            return null;
        }

        [HttpPost]
        public async Task RemoveConnection(string connectionId)
        {
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            user.ConnectionId = "";
            await userManager.UpdateAsync(user);
        }
    }
}

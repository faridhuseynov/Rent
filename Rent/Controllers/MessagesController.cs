﻿using System;
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

namespace Rent.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessageRepository messageRepo;
        private readonly UserManager<User> userManager;

        public MessagesController(IMessageRepository messageRepo, UserManager<User> userManager)
        {
            this.messageRepo = messageRepo;
            this.userManager = userManager;
        }

        // GET: Messages
        [HttpGet]
        public async Task<IActionResult> Inbox()
        {
            var userFromRepo = await userManager.FindByNameAsync(User.Identity.Name);
            var messages = await messageRepo.GetMessagesForUser(userFromRepo.Id);
            var sortedMessageList = messages.OrderByDescending(m => m.MessageSent);
            return View(sortedMessageList);
        }

        // GET: Messages
        [HttpGet]
        public async Task<IActionResult> Outbox()
        {
            var userFromRepo = userManager.FindByNameAsync(User.Identity.Name).Result;
            var messages = await messageRepo.GetMessagesForUser(userFromRepo.Id);
            return View(messages.OrderByDescending(m=>m.MessageSent));
        }

        [HttpGet]
        public async Task<IActionResult> _MessageThread(string recipient, string sender)
        {
            if (recipient!=null && sender!=null)
            {
                ViewBag.Sender = sender;
                ViewBag.Recipient = recipient;
                var userFromRepoSender = await userManager.FindByNameAsync(sender);
                var userFromRepoRecipient = await userManager.FindByNameAsync(recipient);
                var messages =await messageRepo.GetMessageThread(userFromRepoSender.Id, userFromRepoRecipient.Id);
                return PartialView(messages);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task Create(string recipientUserName, string content)
        {
            var recipient = await userManager.FindByNameAsync(recipientUserName);
            var sender = await userManager.FindByNameAsync(User.Identity.Name);
            var date = new DateTime();
            date = DateTime.UtcNow;
            var newMessage = new Message();
            if (recipient!=null && sender!=null)
            {
                var message = new Message
                {
                    RecipientId = recipient.Id, SenderId = sender.Id, Content = content,
                    IsRead = false, DateRead = null, MessageSent = date 
                };
                await messageRepo.AddMessage(message);
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

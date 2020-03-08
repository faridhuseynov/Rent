using System;
using System.Collections.Generic;
using System.Linq;
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
            var userFromRepo = userManager.FindByNameAsync(User.Identity.Name).Result;
            var messages = await messageRepo.GetMessagesForUser(userFromRepo.Id);
            //var test = messages.ToList().Where(u => u.SenderId == userFromRepo.Id);
            //ViewBag["Recipients"] = test.OrderByDescending(m => m.MessageSent).Select(m => m.Recipient).ToList();
            //ViewBag.Received = messages.ToList().Where(u => u.RecipientId == userFromRepo.Id);
            return View(messages.OrderByDescending(m => m.MessageSent));
            //return View(messages);
        }

        // GET: Messages
        [HttpGet]
        public async Task<IActionResult> Outbox()
        {
            var userFromRepo = userManager.FindByNameAsync(User.Identity.Name).Result;
            var messages = await messageRepo.GetMessagesForUser(userFromRepo.Id);
            //var test = messages.ToList().Where(u => u.SenderId == userFromRepo.Id);
            //ViewBag["Recipients"] = test.OrderByDescending(m => m.MessageSent).Select(m => m.Recipient).ToList();
            //ViewBag.Received = messages.ToList().Where(u => u.RecipientId == userFromRepo.Id);
            return View(messages.OrderByDescending(m=>m.MessageSent));
            //return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> MessageThread(string recipient, string sender)
        {
            ViewBag.Sender = sender;
            var userFromRepo = userManager.FindByNameAsync(User.Identity.Name).Result;
            var messages = await messageRepo.GetMessagesForUser(userFromRepo.Id);
            return PartialView("_MessageThread", messages.Where(u=>(u.Recipient.UserName==recipient && u.Sender.UserName==sender)
            || (u.Sender.UserName == recipient && u.Recipient.UserName == sender)));
        }
        [HttpPost]
        public async Task Create(string recipientUserName, string content)
        {
            var recipient = await userManager.FindByNameAsync(recipientUserName);
            var sender = await userManager.FindByNameAsync(User.Identity.Name);
            var date = new DateTime();
            date = DateTime.UtcNow;
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
        // GET: Messages/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var message = await _context.Messages
        //        .Include(m => m.Recipient)
        //        .Include(m => m.Sender)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(message);
        //}

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,SenderId,RecipientId,Content,IsRead,DateRead,MessageSent,SenderDeleted,RecipientDeleted")] Message message)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(message);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id", message.RecipientId);
        //    ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id", message.SenderId);
        //    return View(message);
        //}

       


        //private bool MessageExists(int id)
        //{
        //    return _context.Messages.Any(e => e.Id == id);
        //}
    }
}

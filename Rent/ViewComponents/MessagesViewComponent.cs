using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rent.DomainModels.Models;
using Rent.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rent.ViewComponents
{
    public class MessagesViewComponent : ViewComponent
    {
        private readonly IMessageRepository mr;
        private readonly UserManager<User> um;

        public MessagesViewComponent(IMessageRepository mr, UserManager<User> um)
        {
            this.mr = mr;
            this.um = um;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var user = await um.FindByNameAsync(userName);

            //var messageList = mr.GetMessagesForUser(user.Id).Result.Where(m => m.Recipient.UserName == user.UserName && m.IsRead == false);
            var unreadMessagesCount = mr.GetUnreadInboxMessagesCount(user.Id).Result;
            return View(unreadMessagesCount);
        }
    }
}

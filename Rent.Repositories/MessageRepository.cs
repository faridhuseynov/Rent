using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface IMessageRepository
    {
        Task<int> AddMessage(Message message);
        Task DeleteMessage(int messageId);
        Task<IEnumerable<Message>> GetMessagesForUser(string userId);
        Task<int> GetUnreadInboxMessagesCount(string userId);
        Task<IEnumerable<Message>> GetMessageThread(string senderId, string recipientId);
        Task MarkMessageAsRead(int messageId);
        //int GetUserMessagesCount(string recipientId);

    }
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext db;
        private readonly IConfiguration configuration;
        private readonly string connection;
        public MessageRepository(AppDbContext dbContext, IConfiguration configuration)
        {
            this.db = dbContext;
            this.configuration = configuration;
            connection = this.configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddMessage(Message message)
        {
            await db.Messages.AddAsync(message);
            await db.SaveChangesAsync();
            return message.Id;
        }

        public async Task DeleteMessage(int messageId)
        {
            var message = await db.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
            if (message != null)
            {
                db.Messages.Remove(message);
                await db.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Message>> GetMessagesForUser(string userId)
        {
            return db.Messages.Where(m => m.SenderId == userId || m.RecipientId == userId);
        }
        public async Task<int> GetUnreadInboxMessagesCount(string userId)
        {
            return await db.Messages.Where(m => m.Recipient.Id == userId && m.IsRead == false).CountAsync();
        }

        public async Task<IEnumerable<Message>> GetMessageThread(string userId, string recipientId)
        {
            return db.Messages.Where
                (m => (m.RecipientId == userId && m.SenderId == recipientId && m.RecipientDeleted == false)
                ||
                (m.SenderId == userId && m.RecipientId == recipientId && m.SenderDeleted == false));
        }

        public async Task MarkMessageAsRead(int messageId)
        {
            var message = await db.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (message != null)
            {
                message.IsRead = true;
                await db.SaveChangesAsync();
            }
        }
    }
}

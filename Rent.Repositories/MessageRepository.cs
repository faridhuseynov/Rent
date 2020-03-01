using Microsoft.EntityFrameworkCore;
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
        Task<IEnumerable<Message>> GetMessageThread(string senderId, string recipientId);
        Task MarkMessageAsRead(int messageId);
    }
    public class MessageRepository:IMessageRepository
    {
        private readonly AppDbContext db;
        public MessageRepository(AppDbContext dbContext)
        {
            this.db = dbContext;
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
            if (message!=null)
            {
                db.Messages.Remove(message);
                await db.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Message>> GetMessagesForUser(string userId)
        {
           return db.Messages.Where(m => m.SenderId == userId || m.RecipientId == userId);
        }

        public async Task<IEnumerable<Message>>GetMessageThread(string userId, string recipientId)
        {
            return db.Messages.Where
                (m => (m.RecipientId == userId && m.SenderId == recipientId && m.RecipientDeleted==false)
                ||
                (m.SenderId == userId && m.RecipientId == recipientId && m.SenderDeleted==false));
        }

        public async Task MarkMessageAsRead(int messageId)
        {
            var message =await db.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (message!=null)
            {
                message.IsRead = true;
                await db.SaveChangesAsync();
            }
        }

    }
}

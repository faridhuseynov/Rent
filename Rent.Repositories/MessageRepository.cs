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
        Task<IEnumerable<Message>> GetMessageThread(string senderId, string recipientId);
        Task MarkMessageAsRead(int messageId);
        int GetUserMessagesCount(string recipientId);

    }
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext db;
        private readonly IConfiguration configuration;
        private readonly IHubContext<HubService> hub;
        private readonly string connection;
        public MessageRepository(AppDbContext dbContext, IConfiguration configuration, IHubContext<HubService> hub)
        {
            this.db = dbContext;
            this.configuration = configuration;
            this.hub = hub;
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
        public int GetUserMessagesCount(string recipientId)
        {
            var messagesCount = "";

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                SqlDependency.Start(connection);
                string command = $"select COUNT(RecipientId) as response from dbo.Messages where RecipientId Like '{recipientId}'";
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    SqlDependency dependency = new SqlDependency(cmd);
                    dependency.OnChange += new OnChangeEventHandler(dbChangeNotification);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            messagesCount = reader["response"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            return int.Parse(messagesCount);
        }
        private void dbChangeNotification(object sender, SqlNotificationEventArgs e)
        {
            //hub.Clients.All.SendAsync("updateMessageCount");
            hub.Clients.All.SendAsync("updateMessagesCount");
        }
    }
}

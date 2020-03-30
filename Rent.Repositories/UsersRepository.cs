using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Rent.Repositories
{
    public interface IUsersRepository
    {
        Task AddUser(User user);
        Task UpdateUserDetails(User user);
        //void UpdateUserPassword(User user);
        Task DeleteUser(string userId);
        Task<IEnumerable<User>> GetUsers();
        //IEnumerable<User> GetUserByEmailAndPassword(string email, string passwordHash);
        //IEnumerable<User> GetUserByEmail(string email);
        Task<User> GetUserByUserID(string userID);
        Task<string> GetLatestUserID();

    }
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext db;


        public UsersRepository(AppDbContext dbContext)
        {
            db = dbContext;
        }
        public async Task AddUser(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task DeleteUser(string userId)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
        }

        async public Task<string> GetLatestUserID()
        {
            var user = await db.Users.LastAsync();
            return user.Id;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await db.Users.ToListAsync();
            return users;
        }
        public async Task<User> GetUserByUserID(string userID)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userID);
            return user;
        }

        public async Task UpdateUserDetails(User user)
        {
            var updatedUser = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (updatedUser != null)
            {
                updatedUser.Name = user.Name;
                updatedUser.Surname = user.Surname;
                await db.SaveChangesAsync();
            }
        }
       
    }
}

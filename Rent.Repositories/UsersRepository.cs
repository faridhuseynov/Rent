﻿using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Repositories
{
    public interface IUsersRepository
    {
        void AddUser(User user);
        void UpdateUserDetails(User user);
        //void UpdateUserPassword(User user);
        void DeleteUser(int userId);
        Task<IEnumerable<User>> GetUsers();
        //IEnumerable<User> GetUserByEmailAndPassword(string email, string passwordHash);
        //IEnumerable<User> GetUserByEmail(string email);
        Task<User> GetUserByUserID(int userID);
        Task<int> GetLatestUserID();
    }
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext db;
        public UsersRepository(AppDbContext dbContext)
        {
            db = dbContext;
        }
        async public void AddUser(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        async public void DeleteUser(int userId)
        {
            var user=await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user!=null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
        }

        async public Task<int> GetLatestUserID()
        {
            var user = await db.Users.LastAsync();
            return user.Id;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await db.Users.ToListAsync();
            return users;
        }

        //public async Task<IEnumerable<User>> GetUsersByEmail(string email)
        //{
        //    var user = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
        //}

        //public IEnumerable<User> GetUsersByEmailAndPassword(string email, string passwordHash)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<User> GetUserByUserID(int userID)
        {
            var user =await db.Users.FirstOrDefaultAsync(x => x.Id == userID);
            return user;
        }

        public async void UpdateUserDetails(User user)
        {
            var updatedUser =await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (updatedUser!=null)
            {
                updatedUser.Name = user.Name;
                updatedUser.Surname = user.Surname;
                await db.SaveChangesAsync();
            }
        }

        //public void UpdateUserPassword(User user)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
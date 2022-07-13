using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WcfService1.ChatUOW.EF;
using WcfService1.ChatUOW.Entities;
using WcfService1.ChatUOW.Interfaces;

namespace WcfService1.ChatUOW.Repositories
{
    public class UserRepository:IUserRepository
    {
        private ChatContext db;

        public UserRepository(ChatContext context)
        {
            this.db = context;
        }
        public User Get(int id)
        {
            return db.users.SingleOrDefault(c => c.User_ID == id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.users;
        }

        public User GetUserByConnectionID(string connectionID)
        {
            return db.users.SingleOrDefault(u => u.Connection_Id == connectionID);
        }

        public User GetUserByUserName(string userName)
        {
            return db.users.SingleOrDefault(u => u.UserName == userName);
        }

        public void Insert(User item)
        {
            db.users.Add(item);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
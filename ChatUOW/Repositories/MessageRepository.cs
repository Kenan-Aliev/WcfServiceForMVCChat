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
    public class MessageRepository : IRepository<Messages>
    {
        private ChatContext db;

        public MessageRepository(ChatContext context)
        {
            this.db = context;
        }
        public Messages Get(int id)
        {
            return db.messages.SingleOrDefault(c => c.Message_ID == id);
        }

        public IEnumerable<Messages> GetAll()
        {
            return db.messages;
        }

        public void Insert(Messages item)
        {
            db.messages.Add(item);
        }

        public void Update(Messages item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
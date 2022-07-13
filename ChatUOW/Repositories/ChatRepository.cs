using System;
using System.Collections.Generic;
using WcfService1.ChatUOW.Entities;
using WcfService1.ChatUOW.Interfaces;
using WcfService1.ChatUOW.EF;
using System.Linq;
using System.Data.Entity;

namespace WcfService1.ChatUOW.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private ChatContext db;

        public ChatRepository(ChatContext context)
        {
            this.db = context;
        }
        public Chat Get(int id)
        {
            return db.chats.SingleOrDefault(c => c.Chat_ID == id);
        }

        public IEnumerable<Chat> GetAll()
        {
            return db.chats;
        }

        public Chat GetUsersChat(int userId1, int userId2)
        {
            Chat chat = db.chats.SingleOrDefault(c => (c.User_1 == userId1 || c.User_1 == userId2) && (c.User_2 == userId1 || c.User_2 == userId2));
            return chat;
        }

        public void Insert(Chat item)
        {
            db.chats.Add(item);
        }

        public void Update(Chat item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService1.ChatUOW.EF;
using WcfService1.ChatUOW.Entities;
using WcfService1.ChatUOW.Interfaces;

namespace WcfService1.ChatUOW.Repositories
{
    public class EFUnitOfWork:IUnitOfWork
    {
        private ChatContext db;
        private IUserRepository userRepository;
        private IChatRepository chatRepository;
        private IGroupRepository groupRepository;
        private IRepository<Messages> messageRepository;
        private bool disposed = false;

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }
        public IGroupRepository Groups
        {
            get
            {
                if (groupRepository == null)
                {
                    groupRepository = new GroupRepository(db);
                }
                return groupRepository;
            }
        }
        public IRepository<Messages> Messages
        {
            get
            {
                if (messageRepository == null)
                {
                    messageRepository = new MessageRepository(db);
                }
                return messageRepository;
            }
        }
        public IChatRepository Chats {
            get
            {
                if (chatRepository == null)
                {
                    chatRepository = new ChatRepository(db);
                }
                return chatRepository;
            }
        }

        public EFUnitOfWork()
        {
            db = new ChatContext();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
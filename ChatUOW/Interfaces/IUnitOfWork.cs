using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService1.ChatUOW.Entities;

namespace WcfService1.ChatUOW.Interfaces
{
    interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get;}
        IGroupRepository Groups { get;}
        IRepository<Messages> Messages { get;}
        IChatRepository Chats { get;}

        void Save();
    }
}

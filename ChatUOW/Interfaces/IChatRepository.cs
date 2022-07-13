using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService1.ChatUOW.Entities;

namespace WcfService1.ChatUOW.Interfaces
{
    public interface IChatRepository:IRepository<Chat>
    {
        Chat GetUsersChat(int userId1, int userId2);
    }
}

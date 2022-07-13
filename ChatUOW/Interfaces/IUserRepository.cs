using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService1.ChatUOW.Entities;

namespace WcfService1.ChatUOW.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        User GetUserByUserName(string userName);

        User GetUserByConnectionID(string connectionID);
    }
}

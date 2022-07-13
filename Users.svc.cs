using System.Collections.Generic;
using System.Linq;
using WcfService1.ChatUOW.Entities;
using WcfService1.ChatUOW.Repositories;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Users" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Users.svc или Users.svc.cs в обозревателе решений и начните отладку.
    public class Users : IUsers
    {
        //ChatModels chatModelsContext = new ChatModels();

        EFUnitOfWork chatContext = new EFUnitOfWork();

        public List<User> GetAllUsers(int mainUserID)
        {
            List<User> onlineUsers = chatContext.Users.GetAll().Where(u => u.IsOnline == true && u.User_ID != mainUserID).ToList();
            List<User> users = new List<User>();
            foreach (User user in onlineUsers)
            {
                User User = new User() { User_ID = user.User_ID, UserName = user.UserName, IsOnline = user.IsOnline, Connection_Id = user.Connection_Id };
                users.Add(User);
            }
            return users;
        }
    }
}

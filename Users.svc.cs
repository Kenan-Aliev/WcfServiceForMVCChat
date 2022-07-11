using System.Collections.Generic;
using System.Linq;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Users" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Users.svc или Users.svc.cs в обозревателе решений и начните отладку.
    public class Users : IUsers
    {
        ChatModels chatModelsContext = new ChatModels();

        public List<users> GetAllUsers(int mainUserID)
        {
            List<users> onlineUsers = chatModelsContext.users.Where(u => u.IsOnline == true && u.User_ID != mainUserID).ToList();
            List<users> users = new List<users>();
            foreach (users user in onlineUsers)
            {
                users User = new users() { User_ID = user.User_ID, UserName = user.UserName, IsOnline = user.IsOnline, Connection_Id = user.Connection_Id };
                users.Add(User);
            }
            return users;
        }
    }
}

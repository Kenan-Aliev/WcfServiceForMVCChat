using System;
using System.Linq;
using System.ServiceModel;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class AuthService : IAuthService
    {
        ChatModelsContext chatModelsContext = new ChatModelsContext();

        public users Login(users user)
        {
            users User = chatModelsContext.users.SingleOrDefault(u => u.UserName == user.UserName);
            if (user == null)
            {
                throw new Exception("Пользователь с таким именем не найден");
            }
            if (User.Password != user.Password)
            {
                throw new Exception("Введите верный пароль");
            }
            return User;
        }

        public users Registration(users user)
        {
            users candidate = chatModelsContext.users.SingleOrDefault(u => u.UserName == user.UserName);
            if (candidate != null)
            {
                ServiceError error = new ServiceError() { ErrorCode = 404, Message = "Пользователь с таким именем уже существует" };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            users newUser = new users() { UserName = user.UserName, Password = user.Password };
            chatModelsContext.users.Add(newUser);
            chatModelsContext.SaveChanges();
            return newUser;
        }
    }
}

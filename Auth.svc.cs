using System.ServiceModel;
using WcfService1.ChatUOW.Entities;
using WcfService1.ChatUOW.Repositories;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Auth" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Auth.svc или Auth.svc.cs в обозревателе решений и начните отладку.
    public class Auth : IAuth
    {
        //ChatModels chatModelsContext = new ChatModels();

        EFUnitOfWork chatContext = new EFUnitOfWork();
        public User Login(User user)
        {
            User candidate = chatContext.Users.GetUserByUserName(user.UserName);
            if(candidate == null)
            {
                ServiceError error = new ServiceError() { ErrorCode = 400, Message = "Пользователя с таким именем не существует" };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            if(candidate.Password != user.Password)
            {
                ServiceError error = new ServiceError() { ErrorCode = 400, Message = "Укажите верный пароль" };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            User us = new User() { User_ID = candidate.User_ID, UserName = candidate.UserName};
            return us;
        }

        public User Registration(User user)
        {
            if(user.UserName == null || user.Password == null)
            {
                ServiceError error = new ServiceError() { ErrorCode = 400, Message = "Заполните все поля" };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            User candidate = chatContext.Users.GetUserByUserName(user.UserName);
            if(candidate != null)
            {
                ServiceError error = new ServiceError() { ErrorCode = 400, Message = "Пользователь с таким именем уже существует" };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            User newUser = new User() { UserName = user.UserName, Password = user.Password, IsOnline = false };
            chatContext.Users.Insert(newUser);
            chatContext.Save();
            return newUser;
        }
    }
}

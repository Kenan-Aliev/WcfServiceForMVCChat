using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Auth" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Auth.svc или Auth.svc.cs в обозревателе решений и начните отладку.
    public class Auth : IAuth
    {
        ChatDataBase chatModelsContext = new ChatDataBase();
        public users Login(users user)
        {
            users candidate = chatModelsContext.users.SingleOrDefault(u => u.UserName == user.UserName);
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
            users us = new users() { User_ID = candidate.User_ID, UserName = candidate.UserName};
            return us;
        }

        public users Registration(users user)
        {
            if(user.UserName == null || user.Password == null)
            {
                ServiceError error = new ServiceError() { ErrorCode = 400, Message = "Заполните все поля" };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            users candidate = chatModelsContext.users.SingleOrDefault(u => u.UserName == user.UserName);
            if(candidate != null)
            {
                ServiceError error = new ServiceError() { ErrorCode = 400, Message = "Пользователь с таким именем уже существует" };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            users newUser = new users() { UserName = user.UserName, Password = user.Password, IsOnline = false };
            chatModelsContext.users.Add(newUser);
            chatModelsContext.SaveChangesAsync();
            return newUser;
        }
    }
}

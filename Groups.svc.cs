using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Groups" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Groups.svc или Groups.svc.cs в обозревателе решений и начните отладку.
    public class Groups : IGroups
    {
        ChatModels chatModelsContext = new ChatModels();
        public List<groups> GetUserGroups(int userID)
        {
            var groups = from gr in chatModelsContext.groups
                         where gr.groups_users.Any(grus=>grus.User_ID == userID)
                         select gr;
            List<groups> userGroups = new List<groups>();
            foreach(groups group in groups)
            {
                groups gr = new groups() { Group_ID = group.Group_ID, Group_Name = group.Group_Name };
                userGroups.Add(gr);
            }

            return userGroups;
        }


        public groups CreateNewGroup(int userId , string groupName)
        {
            groups candidate = chatModelsContext.groups.SingleOrDefault(g => g.Group_Name == groupName);
            if(candidate != null)
            {
                ServiceError error = new ServiceError() { Message = "Группа с таким названием уже существует",ErrorCode = 400};
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            users user = chatModelsContext.users.Single(u => u.User_ID == userId);
            groups newGroup = new groups() { Group_Name = groupName };
            chatModelsContext.groups.Add(newGroup);
            chatModelsContext.SaveChanges();
            groups_users groups_Users = new groups_users() { groups = newGroup, users = user};
            user.groups_users.Add(groups_Users);
            chatModelsContext.SaveChanges();
            groups gr = new groups() { Group_ID = newGroup.Group_ID, Group_Name = newGroup.Group_Name };
            return gr;
        }

        public messages SendMessage(int groupId, int fromUserId,string message)
        {
            groups group = chatModelsContext.groups.Single(gr => gr.Group_ID == groupId);
            users user = chatModelsContext.users.Single(u => u.User_ID == fromUserId);
            messages newMessage = new messages() { Group_ID = group.Group_ID, From_User = fromUserId, Send_Date = DateTime.Now, Message = message, IsRead = false,From_UserName = user.UserName };
            chatModelsContext.messages.Add(newMessage);
            chatModelsContext.SaveChanges();
            return new messages() { Message_ID = newMessage.Message_ID,From_User = newMessage.From_User,Send_Date = newMessage.Send_Date,Message = newMessage.Message,IsRead = newMessage.IsRead,From_UserName = newMessage.From_UserName};
        }

        public List<messages> GetGroupMessages(int groupId)
        {
            var messages = from gr in chatModelsContext.groups
                           join message in chatModelsContext.messages
                           on gr.Group_ID equals message.Group_ID
                           where gr.Group_ID == groupId
                           select message;

            List<messages> messagesList = new List<messages>();
            foreach(messages mess in messages)
            {
                messages message = new messages() { Message_ID = mess.Message_ID, From_User = mess.From_User, Send_Date = mess.Send_Date, IsRead = mess.IsRead, Group_ID = mess.Group_ID, Message = mess.Message,From_UserName = mess.From_UserName };
                messagesList.Add(message);
            }
            return messagesList;
        }

        public users AddUserToGroup(int userID,int groupID) 
        {
            var candidate = from us in chatModelsContext.users
                            join grus in chatModelsContext.groups_users
                            on us.User_ID equals grus.User_ID
                            where grus.User_ID == userID && grus.Group_ID == groupID
                            select us;
            if(candidate.Count() > 0)
            {
                ServiceError error = new ServiceError() { Message = "Такой пользователь уже существует в группе", ErrorCode = 400 };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message)); 
            }
            users user = chatModelsContext.users.Single(us => us.User_ID == userID);
            groups group = chatModelsContext.groups.Single(gr => gr.Group_ID == groupID);
            groups_users groups_Users = new groups_users() { groups = group, users = user };
            user.groups_users.Add(groups_Users);
            chatModelsContext.SaveChanges();
            users User = new users() {User_ID = user.User_ID,UserName = user.UserName,Connection_Id = user.Connection_Id,IsOnline = user.IsOnline };
            return User;
        }

        public List<users> GetGroupMembers(int groupId)
        {
            var users = from us in chatModelsContext.users
                        join grus in chatModelsContext.groups_users
                        on us.User_ID equals grus.User_ID
                        where grus.Group_ID == groupId
                        select us;
            List<users> usersList = new List<users>();
            foreach(users u in users)
            {
                users User = new users() { User_ID = u.User_ID, UserName = u.UserName, IsOnline = u.IsOnline, Connection_Id = u.Connection_Id };
                usersList.Add(User);
            }
            return usersList;
        }

    }
}

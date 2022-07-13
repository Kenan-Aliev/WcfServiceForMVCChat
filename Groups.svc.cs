using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.ChatUOW.Entities;
using WcfService1.ChatUOW.Repositories;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Groups" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Groups.svc или Groups.svc.cs в обозревателе решений и начните отладку.
    public class Groups : IGroups
    {
        //ChatModels chatModelsContext = new ChatModels();
        EFUnitOfWork chatContext = new EFUnitOfWork();
        public List<Group> GetUserGroups(int userID)
        {
            var groups = chatContext.Groups.GetUserGroups(userID);
            List<Group> userGroups = new List<Group>();
            foreach(Group group in groups)
            {
                Group gr = new Group() { Group_ID = group.Group_ID, Group_Name = group.Group_Name };
                userGroups.Add(gr);
            }

            return userGroups;
        }


        public Group CreateNewGroup(int userId , string groupName)
        {
            Group candidate = chatContext.Groups.GetGroupByName(groupName);
            if(candidate != null)
            {
                ServiceError error = new ServiceError() { Message = "Группа с таким названием уже существует",ErrorCode = 400};
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message));
            }
            User user = chatContext.Users.Get(userId);
            Group newGroup = new Group() { Group_Name = groupName };
            chatContext.Groups.Insert(newGroup);
            chatContext.Save();
            Group_Users groups_Users = new Group_Users() { group = newGroup, user = user};
            user.group_users.Add(groups_Users);
            chatContext.Save();
            return new Group() { Group_ID = newGroup.Group_ID, Group_Name = newGroup.Group_Name };
        }

        public Messages SendMessage(int groupId, int fromUserId,string message)
        {
            Group group = chatContext.Groups.Get(groupId);
            User user = chatContext.Users.Get(fromUserId);
            Messages newMessage = new Messages() { Group_ID = group.Group_ID, From_User = fromUserId, Send_Date = DateTime.Now, Message = message, IsRead = false,From_UserName = user.UserName };
            chatContext.Messages.Insert(newMessage);
            chatContext.Save();
            return new Messages() { Message_ID = newMessage.Message_ID,From_User = newMessage.From_User,Send_Date = newMessage.Send_Date,Message = newMessage.Message,IsRead = newMessage.IsRead,From_UserName = newMessage.From_UserName};
        }

        public List<Messages> GetGroupMessages(int groupId)
        {
            var messages = chatContext.Groups.GetGroupMessages(groupId);

            List<Messages> messagesList = new List<Messages>();
            foreach(Messages mess in messages)
            {
                Messages message = new Messages() { Message_ID = mess.Message_ID, From_User = mess.From_User, Send_Date = mess.Send_Date, IsRead = mess.IsRead, Group_ID = mess.Group_ID, Message = mess.Message,From_UserName = mess.From_UserName };
                messagesList.Add(message);
            }
            return messagesList;
        }

        public User AddUserToGroup(int userID,int groupID) 
        {
            var candidate = chatContext.Groups.GetGroupMember(groupID, userID);
            if(candidate.Count() > 0)
            {
                ServiceError error = new ServiceError() { Message = "Такой пользователь уже существует в группе", ErrorCode = 400 };
                throw new FaultException<ServiceError>(error, new FaultReason(error.Message)); 
            }
            User user = chatContext.Users.Get(userID);
            Group group = chatContext.Groups.Get(groupID);
            Group_Users groups_Users = new Group_Users() { group = group, user = user };
            user.group_users.Add(groups_Users);
            chatContext.Save();
            return new User() {User_ID = user.User_ID,UserName = user.UserName,Connection_Id = user.Connection_Id,IsOnline = user.IsOnline };
        }

        public List<User> GetGroupMembers(int groupId)
        {
            var users = chatContext.Groups.GetGroupMembers(groupId);
            List<User> usersList = new List<User>();
            foreach(User u in users)
            {
                User User = new User() { User_ID = u.User_ID, UserName = u.UserName, IsOnline = u.IsOnline, Connection_Id = u.Connection_Id };
                usersList.Add(User);
            }
            return usersList;
        }
    }
}

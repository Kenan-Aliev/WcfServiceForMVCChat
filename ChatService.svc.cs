using System;
using System.Collections.Generic;
using System.Linq;
using WcfService1.ChatUOW.Entities;
using WcfService1.ChatUOW.Repositories;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ChatService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы ChatService.svc или ChatService.svc.cs в обозревателе решений и начните отладку.
    public class ChatService : IChatService
    {
        //ChatModels chatModelsContext = new ChatModels();
        EFUnitOfWork chatContext = new EFUnitOfWork();
        public User ClientConnected(string userName,string connectionId)
        {
            User user = chatContext.Users.GetUserByUserName(userName);
            user.IsOnline = true;
            user.Connection_Id = connectionId;
            User us = new User() { IsOnline = user.IsOnline, User_ID = user.User_ID, Connection_Id = user.Connection_Id, UserName = user.UserName };
            chatContext.Users.Update(user);
            chatContext.Save();
            return us;
        }

        public User ClientDisconnected(string ConnectionID)
        {
            User user = chatContext.Users.GetUserByConnectionID(ConnectionID);
            user.IsOnline = false;
            user.Connection_Id = "";
            chatContext.Users.Update(user);
            User us = new User() { IsOnline = user.IsOnline, User_ID = user.User_ID, Connection_Id = user.Connection_Id, UserName = user.UserName };
            chatContext.Save();
            return us;
        }

        public List<Messages> GetAllMessagesWithUser(int user1_id, int user2_id)
        {
            var messagesWithUser = chatContext.Chats.GetAll()
            .Join(chatContext.Messages.GetAll(),
                c => c.Chat_ID,
                m => m.Chat_ID,
                (c, m) => new
                {
                    Chat_ID = c.Chat_ID,
                    MessageID = m.Message_ID,
                    User1 = c.User_1,
                    User2 = c.User_2,
                    Message = m.Message,
                    IsRead = m.IsRead,
                    Send_Date = m.Send_Date,
                    From_User = m.From_User,
                    From_UserName = m.From_UserName
                }).
            Where(a => (a.User1 == user1_id || a.User1 == user2_id) && (a.User2 == user1_id || a.User2 == user2_id));
            List<Messages> messages = new List<Messages>();
            foreach(var m in messagesWithUser)
            {
                Messages newMessage = new Messages() { Message_ID = m.MessageID, Chat_ID = m.Chat_ID, IsRead = m.IsRead, From_User = m.From_User, Send_Date = m.Send_Date, Message = m.Message,From_UserName = m.From_UserName };
                messages.Add(newMessage);
            }
            return messages;
        }

        public Messages SendMessage(int ToUser_ID,string message,int From_User_ID)
        {

            Chat chat = chatContext.Chats.GetUsersChat(ToUser_ID, From_User_ID);
            int chatId = 0;
            if(chat == null)
            {
                Chat newChat = new Chat() { User_1 = From_User_ID, User_2 = ToUser_ID };
                chatContext.Chats.Insert(newChat);
                chatContext.Save();
                chatId = newChat.Chat_ID;
            }
            User user = chatContext.Users.Get(From_User_ID); 
            Messages newMessage = new Messages() { Chat_ID = chat == null ? chatId : chat.Chat_ID, IsRead = false, From_User = From_User_ID, Message = message, Send_Date = DateTime.Now,From_UserName = user.UserName };
            chatContext.Messages.Insert(newMessage);
            chatContext.Save();
            return new Messages() {Message_ID = newMessage.Message_ID, Chat_ID = newMessage.Chat_ID, IsRead = newMessage.IsRead, From_User = newMessage.From_User, Message = newMessage.Message,Send_Date = newMessage.Send_Date,From_UserName = newMessage.From_UserName };
        }
    }
}

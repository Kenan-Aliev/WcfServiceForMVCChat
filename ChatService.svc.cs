using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ChatService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы ChatService.svc или ChatService.svc.cs в обозревателе решений и начните отладку.
    public class ChatService : IChatService
    {
        ChatModels chatModelsContext = new ChatModels();
        public users ClientConnected(string userName,string connectionId)
        {
            users user = chatModelsContext.users.SingleOrDefault(u => u.UserName == userName);
            user.IsOnline = true;
            user.Connection_Id = connectionId;
            users us = new users() { IsOnline = user.IsOnline, User_ID = user.User_ID, Connection_Id = user.Connection_Id, UserName = user.UserName };
            chatModelsContext.SaveChangesAsync();
            return us;
        }

        public users ClientDisconnected(string ConnectionID)
        {
            users user = chatModelsContext.users.SingleOrDefault(u => u.Connection_Id == ConnectionID);
            user.IsOnline = false;
            user.Connection_Id = "";
            
            users us = new users() { IsOnline = user.IsOnline, User_ID = user.User_ID, Connection_Id = user.Connection_Id, UserName = user.UserName };
            chatModelsContext.SaveChangesAsync();
            return us;
        }

        public List<messages> GetAllMessagesWithUser(int user1_id, int user2_id)
        {
            var messagesWithUser = chatModelsContext.chats
            .Join(chatModelsContext.messages,
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
            List<messages> messages = new List<messages>();
            foreach(var m in messagesWithUser)
            {
                messages newMessage = new messages() { Message_ID = m.MessageID, Chat_ID = m.Chat_ID, IsRead = m.IsRead, From_User = m.From_User, Send_Date = m.Send_Date, Message = m.Message,From_UserName = m.From_UserName };
                messages.Add(newMessage);
            }
            return messages;
        }

        public messages SendMessage(int ToUser_ID,string message,int From_User_ID)
        {

            chats chat = chatModelsContext.chats.SingleOrDefault(c => (c.User_1 == ToUser_ID || c.User_1 == From_User_ID) && (c.User_2 == ToUser_ID || c.User_2 == From_User_ID));
            int chatId = 0;
            if(chat == null)
            {
                chats newChat = new chats() { User_1 = From_User_ID, User_2 = ToUser_ID };
                chatModelsContext.chats.Add(newChat);
                chatModelsContext.SaveChanges();
                chatId = newChat.Chat_ID;
            }
            users user = chatModelsContext.users.Single(u => u.User_ID == From_User_ID); 
            messages newMessage = new messages() { Chat_ID = chat == null ? chatId : chat.Chat_ID, IsRead = false, From_User = From_User_ID, Message = message, Send_Date = DateTime.Now,From_UserName = user.UserName };
            chatModelsContext.messages.Add(newMessage);
            chatModelsContext.SaveChanges();
            return new messages() {Message_ID = newMessage.Message_ID, Chat_ID = newMessage.Chat_ID, IsRead = newMessage.IsRead, From_User = newMessage.From_User, Message = newMessage.Message,Send_Date = newMessage.Send_Date,From_UserName = newMessage.From_UserName };
            
        }
    }
}

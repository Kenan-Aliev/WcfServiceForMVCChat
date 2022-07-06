using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IChatService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        users ClientConnected(string userName,string connectionId);

        [OperationContract]
        messages SendMessage(int ToUser_ID, string message, int From_User_ID);

        [OperationContract]
        List<messages> GetAllMessagesWithUser(int user1_id,int user2_id);

        [OperationContract]
        users ClientDisconnected(string ConnectionID);
    }
}

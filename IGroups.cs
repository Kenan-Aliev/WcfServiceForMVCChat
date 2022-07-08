using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IGroups" в коде и файле конфигурации.
    [ServiceContract]
    public interface IGroups
    {
        [OperationContract]
        List<groups> GetUserGroups(int userID);

        [OperationContract]
        [FaultContract(typeof(ServiceError))]
        groups CreateNewGroup(int userId ,string groupName);

        [OperationContract]
        messages SendMessage(int groupId, int fromUserId,string message);

        [OperationContract]
        List<messages> GetGroupMessages(int groupId);

        [OperationContract]
        users AddUserToGroup(int userID, int groupID);

        [OperationContract]
        List<users> GetGroupMembers(int groupId);
    }
}

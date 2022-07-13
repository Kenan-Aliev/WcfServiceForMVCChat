using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.ChatUOW.Entities;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IGroups" в коде и файле конфигурации.
    [ServiceContract]
    public interface IGroups
    {
        [OperationContract]
        List<Group> GetUserGroups(int userID);

        [OperationContract]
        [FaultContract(typeof(ServiceError))]
        Group CreateNewGroup(int userId ,string groupName);

        [OperationContract]
        Messages SendMessage(int groupId, int fromUserId,string message);

        [OperationContract]
        List<Messages> GetGroupMessages(int groupId);

        [OperationContract]
        [FaultContract(typeof(ServiceError))]
        User AddUserToGroup(int userID, int groupID);

        [OperationContract]
        List<User> GetGroupMembers(int groupId);
    }
}

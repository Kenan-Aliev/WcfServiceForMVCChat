using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService1.ChatUOW.Entities;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IAuth" в коде и файле конфигурации.
    [ServiceContract]
    public interface IAuth
    {
        [OperationContract]
        [FaultContract(typeof(ServiceError))]
        User Registration(User user);

        [OperationContract]
        [FaultContract(typeof(ServiceError))]
        User Login(User user);
    }

    [DataContract]
    public class ServiceError
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}

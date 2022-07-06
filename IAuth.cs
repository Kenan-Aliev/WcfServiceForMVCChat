using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IAuth" в коде и файле конфигурации.
    [ServiceContract]
    public interface IAuth
    {
        [OperationContract]
        [FaultContract(typeof(ServiceError))]
        users Registration(users user);

        [OperationContract]
        [FaultContract(typeof(ServiceError))]
        users Login(users user);
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

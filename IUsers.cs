using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IUsers" в коде и файле конфигурации.
    [ServiceContract]
    public interface IUsers
    {
        [OperationContract]
        List<users> GetAllUsers(int mainUserID);
    }
}

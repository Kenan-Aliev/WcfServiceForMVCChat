using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService1.ChatUOW.Entities;

namespace WcfService1.ChatUOW.Interfaces
{
    public interface IGroupRepository:IRepository<Group>
    {
        IQueryable<Group> GetUserGroups(int userID);

        Group GetGroupByName(string groupName);

        IQueryable<Messages> GetGroupMessages(int groupId);

        IQueryable<User> GetGroupMember(int groupId, int userId);

        IQueryable<User> GetGroupMembers(int groupID);
       
    }
}

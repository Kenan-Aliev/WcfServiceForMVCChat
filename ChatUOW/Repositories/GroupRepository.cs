using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WcfService1.ChatUOW.EF;
using WcfService1.ChatUOW.Entities;
using WcfService1.ChatUOW.Interfaces;

namespace WcfService1.ChatUOW.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private ChatContext db;

        public GroupRepository(ChatContext context)
        {
            this.db = context;
        }
        public Group Get(int id)
        {
            return db.groups.SingleOrDefault(c => c.Group_ID == id);
        }

        public IEnumerable<Group> GetAll()
        {
            return db.groups;
        }

        public Group GetGroupByName(string groupName)
        {
            return db.groups.SingleOrDefault(g => g.Group_Name == groupName);
        }

        public IQueryable<User> GetGroupMember(int groupId, int userId)
        {
            var user = from us in db.users
                            join grus in db.groups_users
                            on us.User_ID equals grus.User_ID
                            where grus.User_ID == userId && grus.Group_ID == groupId
                            select us;
            return user;
        }

        public IQueryable<User> GetGroupMembers(int groupID)
        {
            var users = from us in db.users
                        join grus in db.groups_users
                        on us.User_ID equals grus.User_ID
                        where grus.Group_ID == groupID
                        select us;
            return users;
        }

        public IQueryable<Messages> GetGroupMessages(int groupId)
        {
            var messages = from gr in db.groups
                           join message in db.messages
                           on gr.Group_ID equals message.Group_ID
                           where gr.Group_ID == groupId
                           select message;
            return messages;
        }

        public IQueryable<Group> GetUserGroups(int userID)
        {
            var groups = from gr in db.groups
                         where gr.groups_users.Any(grus => grus.User_ID == userID)
                         select gr;
            return groups;
        }

        public void Insert(Group item)
        {
            db.groups.Add(item);
        }

        public void Update(Group item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
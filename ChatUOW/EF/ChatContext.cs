using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WcfService1.ChatUOW.Entities;

namespace WcfService1.ChatUOW.EF
{
    public class ChatContext:DbContext
    {
        public  DbSet<Chat> chats { get; set; }
        public  DbSet<Group> groups { get; set; }
        public  DbSet<Group_Users> groups_users { get; set; }
        public  DbSet<Messages> messages { get; set; }
        public  DbSet<User> users { get; set; }


        public ChatContext() : base("name=ChatModels") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Group>()
            //     .HasMany(e => e.groups_users)
            //     .WithRequired(e => e.group)
            //     .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.chat)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.User_1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.chat1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.User_2)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.group_users)
            //    .WithRequired(e => e.user)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<users>()
            //    .HasMany(e => e.messages)
            //    .WithRequired(e => e.users)
            //    .HasForeignKey(e => e.From_User)
            //    .WillCascadeOnDelete(false);
        }
    }
}
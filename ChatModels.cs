using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WcfService1
{
    public partial class ChatModels : DbContext
    {
        public ChatModels()
            : base("name=ChatModels")
        {
        }

        public virtual DbSet<chats> chats { get; set; }
        public virtual DbSet<groups> groups { get; set; }
        public virtual DbSet<groups_users> groups_users { get; set; }
        public virtual DbSet<messages> messages { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<groups>()
                .HasMany(e => e.groups_users)
                .WithRequired(e => e.groups)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.chats)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.User_1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.chats1)
                .WithRequired(e => e.users1)
                .HasForeignKey(e => e.User_2)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.groups_users)
                .WithRequired(e => e.users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<users>()
                .HasMany(e => e.messages)
                .WithRequired(e => e.users)
                .HasForeignKey(e => e.From_User)
                .WillCascadeOnDelete(false);
        }
    }
}

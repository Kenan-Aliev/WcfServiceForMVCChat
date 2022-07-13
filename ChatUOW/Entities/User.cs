using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WcfService1.ChatUOW.Entities
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Connection_Id { get; set; }

        public bool? IsOnline { get; set; }

        public virtual ICollection<Chat> chat { get; set; }

        public virtual ICollection<Chat> chat1 { get; set; }

        public virtual ICollection<Group_Users> group_users { get; set; }

        public virtual ICollection<Messages> messages { get; set; }
    }
}
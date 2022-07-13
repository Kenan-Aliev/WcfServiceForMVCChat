using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WcfService1.ChatUOW.Entities
{
    public class Chat
    {
        [Key]
        public int Chat_ID { get; set; }

        [Required]
        public int User_1 { get; set; }
        [Required]
        public int User_2 { get; set; }

        public virtual User user { get; set; }

        public virtual User user1 { get; set; }

        public virtual ICollection<Messages> messages { get; set; }
    }
}
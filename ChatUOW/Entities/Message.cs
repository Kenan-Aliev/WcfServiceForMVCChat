using System;
using System.ComponentModel.DataAnnotations;

namespace WcfService1.ChatUOW.Entities
{
    public class Messages
    {
        [Key]
        public int Message_ID { get; set; }

        public int? Chat_ID { get; set; }

        public int? Group_ID { get; set; }

        public int From_User { get; set; }

        [Required]
        [StringLength(30)]
        public string From_UserName { get; set; }

        public DateTime Send_Date { get; set; }

        public bool? IsRead { get; set; }

        [Required]
        [StringLength(100)]
        public string  Message{ get; set; }

        public virtual Chat chat { get; set; }

        public virtual Group group { get; set; }

        public virtual User user{ get; set; }
    }
}
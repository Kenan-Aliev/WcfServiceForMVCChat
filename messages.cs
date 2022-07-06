namespace WcfService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class messages
    {
        [Key]
        public int Message_ID { get; set; }

        public int Chat_ID { get; set; }

        public int From_User { get; set; }

        public DateTime Send_Date { get; set; }

        public bool? IsRead { get; set; }

        [Required]
        [StringLength(100)]
        public string Message { get; set; }

        public virtual chats chats { get; set; }

        public virtual users users { get; set; }
    }
}

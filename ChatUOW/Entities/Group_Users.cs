using System.ComponentModel.DataAnnotations;

namespace WcfService1.ChatUOW.Entities
{
    public class Group_Users
    {
        [Key]
        public int Groups_Users_ID { get; set; }
        [Required]
        public int Group_ID { get; set; }
        [Required]
        public int User_ID { get; set; }

        public virtual Group group { get; set; }

        public virtual User user { get; set; }
    }
}
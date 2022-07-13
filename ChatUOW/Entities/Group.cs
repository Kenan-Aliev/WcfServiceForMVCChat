using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WcfService1.ChatUOW.Entities
{
    public class Group
    {
        [Key]
        public int Group_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Group_Name { get; set; }

        public virtual ICollection<Group_Users> groups_users { get; set; }

        public virtual ICollection<Messages> messages { get; set; }
    }
}
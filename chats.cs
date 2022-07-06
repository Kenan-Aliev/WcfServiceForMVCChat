namespace WcfService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class chats
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public chats()
        {
            messages = new HashSet<messages>();
        }

        [Key]
        public int Chat_ID { get; set; }

        public int User_1 { get; set; }

        public int User_2 { get; set; }

        public virtual users users { get; set; }

        public virtual users users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<messages> messages { get; set; }
    }
}

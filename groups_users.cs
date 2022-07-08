namespace WcfService1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class groups_users
    {
        [Key]
        public int Groups_Users_ID { get; set; }

        public int? Group_ID { get; set; }

        public int? User_ID { get; set; }

        public virtual groups groups { get; set; }

        public virtual users users { get; set; }
    }
}

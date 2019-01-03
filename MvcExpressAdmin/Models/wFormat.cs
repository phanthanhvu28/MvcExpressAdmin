namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wFormat")]
    public partial class wFormat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int mID { get; set; }

        public int? TID { get; set; }

        public bool? FRight { get; set; }

        public bool? UpperVideo { get; set; }

        public virtual wMenu1 wMenu1 { get; set; }

        public virtual wTypeFormat wTypeFormat { get; set; }
    }
}

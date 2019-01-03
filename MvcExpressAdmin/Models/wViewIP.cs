namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wViewIP")]
    public partial class wViewIP
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string IP { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime ViewDate { get; set; }
    }
}

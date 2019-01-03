namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GhiChuCT")]
    public partial class GhiChuCT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string TID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string LID { get; set; }

        [StringLength(200)]
        public string TName { get; set; }
    }
}

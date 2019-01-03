namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GhiChu")]
    public partial class GhiChu
    {
        [Key]
        [StringLength(50)]
        public string TID { get; set; }

        public bool? HienThi { get; set; }
    }
}

namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhomNhanVienCT")]
    public partial class NhomNhanVienCT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string NhomNVID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string LID { get; set; }

        [StringLength(50)]
        public string TenNhom { get; set; }
    }
}

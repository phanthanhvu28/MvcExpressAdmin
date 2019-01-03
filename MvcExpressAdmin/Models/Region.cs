namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Region
    {
        [Key]
        [StringLength(50)]
        public string MaNuoc { get; set; }

        [StringLength(150)]
        public string TenNuoc { get; set; }

        public int? ThuTu { get; set; }

        [StringLength(500)]
        public string GMT { get; set; }

        public bool? HienThi { get; set; }
    }
}

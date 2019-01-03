namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mApp")]
    public partial class mApp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AID { get; set; }

        [StringLength(50)]
        public string MaNuoc { get; set; }

        public int? NewspaperId { get; set; }

        [StringLength(100)]
        public string AccountID { get; set; }

        [StringLength(200)]
        public string TenApp { get; set; }

        [StringLength(500)]
        public string Icon512 { get; set; }

        [StringLength(500)]
        public string Image1024 { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        public string NoiDung { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [StringLength(500)]
        public string LinkStore { get; set; }

        [StringLength(500)]
        public string PackageName { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        public DateTime? NgayCN { get; set; }

        public int? MaNV { get; set; }

        public int? FID { get; set; }

        public int? STT { get; set; }

        public bool? HieuLuc { get; set; }
    }
}

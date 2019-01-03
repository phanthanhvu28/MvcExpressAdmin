namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mPerThuMuc")]
    public partial class mPerThuMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNV { get; set; }

        [StringLength(100)]
        public string ThuMuc { get; set; }

        public bool? HieuLuc { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}

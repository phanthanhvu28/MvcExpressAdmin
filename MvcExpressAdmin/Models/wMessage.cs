namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wMessage")]
    public partial class wMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CID { get; set; }

        [StringLength(200)]
        public string HoTen { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string DienThoai { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        [StringLength(500)]
        public string NoiDung { get; set; }

        public DateTime? NgayGui { get; set; }

        public string Reply { get; set; }

        public DateTime? NgayReply { get; set; }

        public int? MaNV { get; set; }

        public bool TrangThai { get; set; }
    }
}

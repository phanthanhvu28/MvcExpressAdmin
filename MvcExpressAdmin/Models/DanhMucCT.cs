namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucCT")]
    public partial class DanhMucCT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDM { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string LID { get; set; }

        [StringLength(50)]
        public string TenDM { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}

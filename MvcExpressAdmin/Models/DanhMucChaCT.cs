namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucChaCT")]
    public partial class DanhMucChaCT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDMC { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string LID { get; set; }

        [StringLength(100)]
        public string TenDMC { get; set; }
    }
}

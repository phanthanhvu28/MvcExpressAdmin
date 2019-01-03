namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wService")]
    public partial class wService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SID { get; set; }

        [StringLength(200)]
        public string TenDV { get; set; }

        [StringLength(200)]
        public string Icon { get; set; }

        [StringLength(50)]
        public string ahref { get; set; }

        public string MoTa { get; set; }

        public string NoiDung { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public bool? HienThi { get; set; }

        public int? STT { get; set; }
    }
}

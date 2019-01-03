namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wVideo")]
    public partial class wVideo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int vID { get; set; }

        [StringLength(200)]
        public string vTitle { get; set; }

        [StringLength(500)]
        public string vURL { get; set; }

        [StringLength(50)]
        public string vType { get; set; }

        public DateTime? vDate { get; set; }

        public int? vTT { get; set; }

        public bool? vDisplay { get; set; }
    }
}

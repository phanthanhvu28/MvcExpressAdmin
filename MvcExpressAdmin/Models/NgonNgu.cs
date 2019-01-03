namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NgonNgu")]
    public partial class NgonNgu
    {
        [Key]
        [StringLength(50)]
        public string LID { get; set; }

        [StringLength(50)]
        public string LName { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        [StringLength(200)]
        public string Class { get; set; }

        public bool? LShow { get; set; }

        public int? ThuTu { get; set; }
    }
}

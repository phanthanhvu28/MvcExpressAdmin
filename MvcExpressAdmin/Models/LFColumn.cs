namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LFColumn")]
    public partial class LFColumn
    {
        [Key]
        [StringLength(50)]
        public string IdFc { get; set; }

        [StringLength(150)]
        public string FormatColumn { get; set; }
    }
}

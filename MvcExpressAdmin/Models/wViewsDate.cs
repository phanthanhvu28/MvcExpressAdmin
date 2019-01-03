namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wViewsDate")]
    public partial class wViewsDate
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime ViewDate { get; set; }

        public int? CountView { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Device { get; set; }
    }
}

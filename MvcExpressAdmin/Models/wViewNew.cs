namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class wViewNew
    {
        [Key]
        [StringLength(50)]
        public string rssID { get; set; }

        public int? CountView { get; set; }
    }
}

namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LNew
    {
        [Key]
        [StringLength(50)]
        public string IDLNews { get; set; }

        public int? IDMenu { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(150)]
        public string rssDate { get; set; }

        [StringLength(500)]
        public string IconItem { get; set; }

        [StringLength(500)]
        public string LinkItem { get; set; }

        [StringLength(500)]
        public string LinkVideo { get; set; }

        public string Summary { get; set; }

        public DateTime? updDate { get; set; }

        public int? MaNV { get; set; }
    }
}

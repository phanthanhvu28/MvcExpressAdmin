namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mNew
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewsId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Summary { get; set; }

        [StringLength(500)]
        public string Icon { get; set; }

        public int? NewspaperMenuId { get; set; }

        public DateTime? DatePost { get; set; }

        public bool? Display { get; set; }

        public int? UserID { get; set; }
    }
}

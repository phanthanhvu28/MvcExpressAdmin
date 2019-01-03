namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class wNew
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nID { get; set; }

        public int? sID { get; set; }

        public int? NewspaperId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Detail { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateNews { get; set; }

        public DateTime? DatePost { get; set; }

        public bool? HotNews { get; set; }

        public bool? PopularNews { get; set; }

        public bool? MoreNews { get; set; }

        public bool? Effect { get; set; }

        public int? MaNV { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        public virtual mNewsMenu mNewsMenu { get; set; }

        public virtual mNewspaper mNewspaper { get; set; }

        public virtual wMenu2 wMenu2 { get; set; }
    }
}

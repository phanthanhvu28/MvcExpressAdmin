namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class rssNew
    {
        [Key]
        [StringLength(50)]
        public string rssID { get; set; }

        public int? NewspaperMenuId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string IconRss { get; set; }

        [StringLength(500)]
        public string IconSave { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }

        [StringLength(100)]
        public string rssDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime? DateInput { get; set; }

        public bool? HotNews { get; set; }

        public bool? PopularNews { get; set; }

        public bool? MoreNews { get; set; }

        public bool? Effect { get; set; }

        public int? MaNV { get; set; }

        public virtual mNewspaperMenu mNewspaperMenu { get; set; }
    }
}

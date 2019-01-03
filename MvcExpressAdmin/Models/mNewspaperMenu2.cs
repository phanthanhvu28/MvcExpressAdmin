namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mNewspaperMenu2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewspaperMenuId { get; set; }

        public int? NewspaperId { get; set; }

        [StringLength(3000)]
        public string Title { get; set; }

        public bool? Display { get; set; }

        public int? Stt { get; set; }

        [StringLength(200)]
        public string rssLink { get; set; }

        public virtual mNewspaper2 mNewspaper2 { get; set; }
    }
}

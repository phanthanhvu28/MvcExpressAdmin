namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mNewspaperMenu")]
    public partial class mNewspaperMenu
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public mNewspaperMenu()
        //{
        //    mNewsMenus = new HashSet<mNewsMenu>();
        //    rssNews = new HashSet<rssNew>();
        //    wMenu2 = new HashSet<wMenu2>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewspaperMenuId { get; set; }

        public int? NewspaperId { get; set; }

        [StringLength(3000)]
        public string Title { get; set; }

        public bool? Display { get; set; }

        public int? Stt { get; set; }

        [StringLength(300)]
        public string rssLink { get; set; }

        public bool? TopMenu { get; set; }

        public bool? GroupApp { get; set; }

        public bool? Video { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<mNewsMenu> mNewsMenus { get; set; }

        //public virtual mNewspaper mNewspaper { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<rssNew> rssNews { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<wMenu2> wMenu2 { get; set; }
    }
}

namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mNewspaper")]
    public partial class mNewspaper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mNewspaper()
        {
            mNewspaperMenus = new HashSet<mNewspaperMenu>();
            //wNews = new HashSet<wNew>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewspaperId { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Icon { get; set; }

        public bool? Display { get; set; }

        public int? Stt { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [StringLength(50)]
        public string Languge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mNewspaperMenu> mNewspaperMenus { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<wNew> wNews { get; set; }
    }
}

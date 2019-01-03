namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class wMenu2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public wMenu2()
        {
            wNews = new HashSet<wNew>();
            mNewspaperMenus = new HashSet<mNewspaperMenu>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sID { get; set; }

        public int? mID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public bool? Effect { get; set; }

        public int? STT { get; set; }

        public virtual wMenu1 wMenu1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wNew> wNews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mNewspaperMenu> mNewspaperMenus { get; set; }
    }
}

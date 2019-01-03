namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mTypeApp")]
    public partial class mTypeApp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mTypeApp()
        {
            mCateApps = new HashSet<mCateApp>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int? FID { get; set; }

        public bool? Effect { get; set; }

        public int? STT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mCateApp> mCateApps { get; set; }

        public virtual mFlatform mFlatform { get; set; }
    }
}

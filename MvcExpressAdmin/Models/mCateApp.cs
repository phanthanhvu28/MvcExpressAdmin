namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mCateApp")]
    public partial class mCateApp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CID { get; set; }

        public int TID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public bool? Effect { get; set; }

        public int? STT { get; set; }

        public virtual mTypeApp mTypeApp { get; set; }
    }
}

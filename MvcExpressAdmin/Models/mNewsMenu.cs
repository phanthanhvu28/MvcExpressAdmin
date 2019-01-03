namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mNewsMenu")]
    public partial class mNewsMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nID { get; set; }

        public int? NewspaperMenuId { get; set; }

        public virtual mNewspaperMenu mNewspaperMenu { get; set; }

        public virtual wNew wNew { get; set; }
    }
}

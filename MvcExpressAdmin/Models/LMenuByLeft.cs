namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LMenuByLeft")]
    public partial class LMenuByLeft
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDMenu { get; set; }

        public int? IDMenuLeft { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        public bool? Display { get; set; }

        public bool? DisplayWeb { get; set; }

        public bool? DisplayHome { get; set; }

        [StringLength(50)]
        public string IdFc { get; set; }

        [StringLength(500)]
        public string TitleWeb { get; set; }

        public int? STTWeb { get; set; }

        public int? STT { get; set; }
    }
}

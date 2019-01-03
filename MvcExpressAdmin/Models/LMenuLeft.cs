namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LMenuLeft")]
    public partial class LMenuLeft
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDMenuLeft { get; set; }

        public int? NewspaperId { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public bool? Display { get; set; }

        public int? STT { get; set; }
    }
}

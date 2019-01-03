namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountStore")]
    public partial class AccountStore
    {
        [Key]
        [StringLength(100)]
        public string AccountID { get; set; }

        [StringLength(500)]
        public string AccountName { get; set; }

        public int? STT { get; set; }

        public bool? Effect { get; set; }
    }
}

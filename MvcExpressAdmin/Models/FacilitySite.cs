namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FacilitySite
    {
        [Key]
        [Column(Order = 0)]
        public Guid FacilitySiteID { get; set; }

        public string FacilityName { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool IsActive { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CreatedBy { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CreatedAt { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsDeleted { get; set; }
    }
}

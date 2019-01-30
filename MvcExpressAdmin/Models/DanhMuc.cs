namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMuc")]
    public partial class DanhMuc
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public DanhMuc()
        //{
        //    //ChucNangs = new HashSet<ChucNang>();
        //    //DanhMucCTs = new HashSet<DanhMucCT>();
        //   // NhanViens = new HashSet<NhanVien>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDM { get; set; }

        public int? MaDMC { get; set; }

        [StringLength(50)]
        public string Site { get; set; }

        public bool? HienThi { get; set; }

        public int? ThuTu { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ChucNang> ChucNangs { get; set; }

        //public virtual DanhMucCha DanhMucCha { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DanhMucCT> DanhMucCTs { get; set; }

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       // public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}

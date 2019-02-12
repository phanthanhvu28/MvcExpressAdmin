namespace MvcExpressAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public NhanVien()
        //{
        //    NhanVienChucNangs = new HashSet<NhanVienChucNang>();
        //    DanhMucs = new HashSet<DanhMuc>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNV { get; set; }

        [StringLength(10)]
        public string SoThe { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        public int? GioiTinh { get; set; }

        [StringLength(50)]
        public string NhomNVID { get; set; }

        public bool? HienThi { get; set; }

        public virtual DangNhap DangNhap { get; set; }

        public virtual mPerThuMuc mPerThuMuc { get; set; }

        public virtual NhomNhanVien NhomNhanVien { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<NhanVienChucNang> NhanVienChucNangs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DanhMuc> DanhMucs { get; set; }
    }
}

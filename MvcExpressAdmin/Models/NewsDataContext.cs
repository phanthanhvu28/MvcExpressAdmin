namespace MvcExpressAdmin.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewsDataContext : DbContext
    {
        public NewsDataContext()
            : base("name=NewsDataContext")
        {
        }

        public virtual DbSet<AccountStore> AccountStores { get; set; }
        public virtual DbSet<ChucNang> ChucNangs { get; set; }
        public virtual DbSet<DangNhap> DangNhaps { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DanhMucCha> DanhMucChas { get; set; }
        public virtual DbSet<DanhMucChaCT> DanhMucChaCTs { get; set; }
        public virtual DbSet<DanhMucCT> DanhMucCTs { get; set; }
        public virtual DbSet<GhiChu> GhiChus { get; set; }
        public virtual DbSet<GhiChuCT> GhiChuCTs { get; set; }
        public virtual DbSet<GioiTinh> GioiTinhs { get; set; }
        public virtual DbSet<LConfigMenu> LConfigMenus { get; set; }
        public virtual DbSet<LFColumn> LFColumns { get; set; }
        public virtual DbSet<LMenuByLeft> LMenuByLefts { get; set; }
        public virtual DbSet<LMenuLeft> LMenuLefts { get; set; }
        public virtual DbSet<LNew> LNews { get; set; }
        public virtual DbSet<mApp> mApps { get; set; }
        public virtual DbSet<mCateApp> mCateApps { get; set; }
        public virtual DbSet<mFlatform> mFlatforms { get; set; }
        public virtual DbSet<mNew> mNews { get; set; }
        public virtual DbSet<mNewsMenu> mNewsMenus { get; set; }
        public virtual DbSet<mNewspaper> mNewspapers { get; set; }
        public virtual DbSet<mNewspaper2> mNewspaper2 { get; set; }
        public virtual DbSet<mNewspaperMenu> mNewspaperMenus { get; set; }
        public virtual DbSet<mNewspaperMenu2> mNewspaperMenu2 { get; set; }
        public virtual DbSet<mPerThuMuc> mPerThuMucs { get; set; }
        public virtual DbSet<mProcess> mProcesses { get; set; }
        public virtual DbSet<mTypeApp> mTypeApps { get; set; }
        public virtual DbSet<NgonNgu> NgonNgus { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhanVienChucNang> NhanVienChucNangs { get; set; }
        public virtual DbSet<NhomNhanVien> NhomNhanViens { get; set; }
        public virtual DbSet<NhomNhanVienCT> NhomNhanVienCTs { get; set; }

        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<PhanBoPB> PhanBoPBs { get; set; }
        
        public virtual DbSet<PhanQuyenTB> PhanQuyenTBs { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<rssNew> rssNews { get; set; }
        public virtual DbSet<wFormat> wFormats { get; set; }
        public virtual DbSet<wMenu1> wMenu1 { get; set; }
        public virtual DbSet<wMenu2> wMenu2 { get; set; }
        public virtual DbSet<wMessage> wMessages { get; set; }
        public virtual DbSet<wNew> wNews { get; set; }
        public virtual DbSet<wNhanVienMenu> wNhanVienMenus { get; set; }
        public virtual DbSet<wService> wServices { get; set; }
        public virtual DbSet<wSlider> wSliders { get; set; }
        public virtual DbSet<wTypeFormat> wTypeFormats { get; set; }
        public virtual DbSet<wVideo> wVideos { get; set; }
        public virtual DbSet<wViewIP> wViewIPs { get; set; }
        public virtual DbSet<wViewsDate> wViewsDates { get; set; }
        public virtual DbSet<FacilitySite> FacilitySites { get; set; }
        public virtual DbSet<wViewNew> wViewNews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountStore>()
                .Property(e => e.AccountID)
                .IsUnicode(false);

            modelBuilder.Entity<ChucNang>()
                .Property(e => e.MaCN)
                .IsUnicode(false);

            modelBuilder.Entity<ChucNang>()
                .Property(e => e.LID)
                .IsUnicode(false);

            modelBuilder.Entity<DangNhap>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<DangNhap>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.Site)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.Icon)
                .IsUnicode(false);

            //modelBuilder.Entity<DanhMuc>()
            //    .HasMany(e => e.ChucNangs)
            //    .WithRequired(e => e.DanhMuc)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<DanhMuc>()
            //    .HasMany(e => e.DanhMucCTs)
            //    .WithRequired(e => e.DanhMuc)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<DanhMuc>()
            //    .HasMany(e => e.NhanViens)
            //    .WithMany(e => e.DanhMucs)
            //    .Map(m => m.ToTable("PhanQuyen").MapLeftKey("MaDM").MapRightKey("MaNV"));

            modelBuilder.Entity<DanhMucCha>()
                .Property(e => e.CssClass)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucChaCT>()
                .Property(e => e.LID)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucCT>()
                .Property(e => e.LID)
                .IsUnicode(false);

            modelBuilder.Entity<GhiChu>()
                .Property(e => e.TID)
                .IsUnicode(false);

            modelBuilder.Entity<GhiChuCT>()
                .Property(e => e.TID)
                .IsUnicode(false);

            modelBuilder.Entity<GhiChuCT>()
                .Property(e => e.LID)
                .IsUnicode(false);

            modelBuilder.Entity<LFColumn>()
                .Property(e => e.IdFc)
                .IsUnicode(false);

            modelBuilder.Entity<LMenuByLeft>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<LMenuByLeft>()
                .Property(e => e.IdFc)
                .IsUnicode(false);

            modelBuilder.Entity<LNew>()
                .Property(e => e.IDLNews)
                .IsUnicode(false);

            modelBuilder.Entity<LNew>()
                .Property(e => e.IconItem)
                .IsUnicode(false);

            modelBuilder.Entity<LNew>()
                .Property(e => e.LinkItem)
                .IsUnicode(false);

            modelBuilder.Entity<LNew>()
                .Property(e => e.LinkVideo)
                .IsUnicode(false);

            modelBuilder.Entity<mApp>()
                .Property(e => e.MaNuoc)
                .IsUnicode(false);

            modelBuilder.Entity<mApp>()
                .Property(e => e.AccountID)
                .IsUnicode(false);

            modelBuilder.Entity<mNewspaper>()
                .Property(e => e.Languge)
                .IsUnicode(false);

            modelBuilder.Entity<mNewspaperMenu>()
                .HasMany(e => e.wMenu2)
                .WithMany(e => e.mNewspaperMenus)
                .Map(m => m.ToTable("wNewsMenuId").MapLeftKey("NewspaperMenuId").MapRightKey("sID"));

            modelBuilder.Entity<mTypeApp>()
                .HasMany(e => e.mCateApps)
                .WithRequired(e => e.mTypeApp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NgonNgu>()
                .Property(e => e.LID)
                .IsUnicode(false);

            modelBuilder.Entity<NgonNgu>()
                .Property(e => e.Icon)
                .IsUnicode(false);

            modelBuilder.Entity<NgonNgu>()
                .Property(e => e.Class)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SoThe)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.NhomNVID)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasOptional(e => e.DangNhap)
                .WithRequired(e => e.NhanVien);

            modelBuilder.Entity<NhanVien>()
                .HasOptional(e => e.mPerThuMuc)
                .WithRequired(e => e.NhanVien);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.NhanVienChucNangs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVienChucNang>()
                .Property(e => e.MaCN)
                .IsUnicode(false);

            modelBuilder.Entity<NhomNhanVien>()
                .Property(e => e.NhomNVID)
                .IsUnicode(false);

            modelBuilder.Entity<NhomNhanVienCT>()
                .Property(e => e.NhomNVID)
                .IsUnicode(false);

            modelBuilder.Entity<NhomNhanVienCT>()
                .Property(e => e.LID)
                .IsUnicode(false);

            modelBuilder.Entity<PhanBoPB>()
                .Property(e => e.NhomNVID)
                .IsUnicode(false);

            modelBuilder.Entity<Region>()
                .Property(e => e.MaNuoc)
                .IsUnicode(false);

            modelBuilder.Entity<rssNew>()
                .Property(e => e.rssID)
                .IsUnicode(false);

            modelBuilder.Entity<wMenu1>()
                .Property(e => e.Folder)
                .IsUnicode(false);

            modelBuilder.Entity<wMenu1>()
                .HasOptional(e => e.wFormat)
                .WithRequired(e => e.wMenu1);

            modelBuilder.Entity<wNew>()
                .HasOptional(e => e.mNewsMenu)
                .WithRequired(e => e.wNew);

            modelBuilder.Entity<wService>()
                .Property(e => e.Icon)
                .IsUnicode(false);

            modelBuilder.Entity<wService>()
                .Property(e => e.ahref)
                .IsUnicode(false);

            modelBuilder.Entity<wService>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<wSlider>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<wVideo>()
                .Property(e => e.vType)
                .IsUnicode(false);

            modelBuilder.Entity<wViewIP>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<wViewsDate>()
                .Property(e => e.Device)
                .IsUnicode(false);

            modelBuilder.Entity<wViewNew>()
                .Property(e => e.rssID)
                .IsUnicode(false);
        }
    }
}

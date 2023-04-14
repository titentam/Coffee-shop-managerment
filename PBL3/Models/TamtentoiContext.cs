using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PBL3.Models
{
    public partial class TamtentoiContext : DbContext
    {
        public TamtentoiContext()
        {
        }

        public TamtentoiContext(DbContextOptions<TamtentoiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ban> Bans { get; set; } = null!;
        public virtual DbSet<CaLam> CaLams { get; set; } = null!;
        public virtual DbSet<CongThuc> CongThucs { get; set; } = null!;
        public virtual DbSet<CongThucNguyenLieu> CongThucNguyenLieus { get; set; } = null!;
        public virtual DbSet<DatHang> DatHangs { get; set; } = null!;
        public virtual DbSet<DatHangNguyenLieu> DatHangNguyenLieus { get; set; } = null!;
        public virtual DbSet<DonDatMon> DonDatMons { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<LoaiMon> LoaiMons { get; set; } = null!;
        public virtual DbSet<LoaiNhanVien> LoaiNhanViens { get; set; } = null!;
        public virtual DbSet<Mon> Mons { get; set; } = null!;
        public virtual DbSet<MonDonDatMon> MonDonDatMons { get; set; } = null!;
        public virtual DbSet<NguyenLieu> NguyenLieus { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-CUA-DUY\\SQLEXPRESS02;Database=Tamtentoi;Trusted_Connection=True;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ban>(entity =>
            {
                entity.ToTable("Ban");

                entity.Property(e => e.BanId).HasColumnName("BanID");
            });

            modelBuilder.Entity<CaLam>(entity =>
            {
                entity.HasKey(e => e.CaId)
                    .HasName("PK__CaLam__A679D9A085B25305");

                entity.ToTable("CaLam");

                entity.Property(e => e.CaId).HasColumnName("CaID");

                entity.Property(e => e.TenCa).HasMaxLength(50);

                entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");
            });

            modelBuilder.Entity<CongThuc>(entity =>
            {
                entity.ToTable("CongThuc");

                entity.Property(e => e.CongThucId).HasColumnName("CongThucID");

                entity.Property(e => e.TenCongThuc).HasMaxLength(50);
            });

            modelBuilder.Entity<CongThucNguyenLieu>(entity =>
            {
                entity.HasKey(e => new { e.NguyenLieuId, e.CongThucId })
                    .HasName("PK__CongThuc__99C3BD5944E54055");

                entity.ToTable("CongThuc_NguyenLieu");

                entity.Property(e => e.NguyenLieuId).HasColumnName("NguyenLieuID");

                entity.Property(e => e.CongThucId).HasColumnName("CongThucID");

                entity.HasOne(d => d.CongThuc)
                    .WithMany(p => p.CongThucNguyenLieus)
                    .HasForeignKey(d => d.CongThucId)
                    .HasConstraintName("FK__CongThuc___CongT__37A5467C");

                entity.HasOne(d => d.NguyenLieu)
                    .WithMany(p => p.CongThucNguyenLieus)
                    .HasForeignKey(d => d.NguyenLieuId)
                    .HasConstraintName("FK__CongThuc___Nguye__36B12243");
            });

            modelBuilder.Entity<DatHang>(entity =>
            {
                entity.ToTable("DatHang");

                entity.Property(e => e.DathangId).HasColumnName("DathangID");

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.Property(e => e.NgayDat).HasColumnType("datetime");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.DatHangs)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__DatHang__NhanVie__49C3F6B7");
            });

            modelBuilder.Entity<DatHangNguyenLieu>(entity =>
            {
                entity.HasKey(e => new { e.NguyenLieuId, e.DathangId })
                    .HasName("PK__DatHang___F309F0923294BAEB");

                entity.ToTable("DatHang_NguyenLieu");

                entity.Property(e => e.NguyenLieuId).HasColumnName("NguyenLieuID");

                entity.Property(e => e.DathangId).HasColumnName("DathangID");

                entity.Property(e => e.TongGia).HasColumnType("money");

                entity.HasOne(d => d.Dathang)
                    .WithMany(p => p.DatHangNguyenLieus)
                    .HasForeignKey(d => d.DathangId)
                    .HasConstraintName("FK__DatHang_N__Datha__4D94879B");

                entity.HasOne(d => d.NguyenLieu)
                    .WithMany(p => p.DatHangNguyenLieus)
                    .HasForeignKey(d => d.NguyenLieuId)
                    .HasConstraintName("FK__DatHang_N__Nguye__4CA06362");
            });

            modelBuilder.Entity<DonDatMon>(entity =>
            {
                entity.ToTable("DonDatMon");

                entity.Property(e => e.DonDatMonId).HasColumnName("DonDatMonID");

                entity.Property(e => e.BanId).HasColumnName("BanID");

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.Property(e => e.HoaDonId).HasColumnName("HoaDonID");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.HasOne(d => d.Ban)
                    .WithMany(p => p.DonDatMons)
                    .HasForeignKey(d => d.BanId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__DonDatMon__BanID__45F365D3");

                entity.HasOne(d => d.HoaDon)
                    .WithMany(p => p.DonDatMons)
                    .HasForeignKey(d => d.HoaDonId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__DonDatMon__HoaDo__44FF419A");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.DonDatMons)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__DonDatMon__NhanV__46E78A0C");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.ToTable("HoaDon");

                entity.Property(e => e.HoaDonId).HasColumnName("HoaDonID");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.ThoiGianTao).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__HoaDon__NhanVien__4222D4EF");
            });

            modelBuilder.Entity<LoaiMon>(entity =>
            {
                entity.ToTable("LoaiMon");

                entity.Property(e => e.LoaiMonId).HasColumnName("LoaiMonID");

                entity.Property(e => e.TenLoaiMon).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiNhanVien>(entity =>
            {
                entity.HasKey(e => e.LoaiNv)
                    .HasName("PK__LoaiNhan__4824B97A2A111614");

                entity.ToTable("LoaiNhanVien");

                entity.Property(e => e.LoaiNv).HasColumnName("LoaiNV");

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<Mon>(entity =>
            {
                entity.ToTable("Mon");

                entity.Property(e => e.MonId).HasColumnName("MonID");

                entity.Property(e => e.CongThucId).HasColumnName("CongThucID");

                entity.Property(e => e.Gia).HasColumnType("money");

                entity.Property(e => e.HinhAnh).HasMaxLength(50);

                entity.Property(e => e.LoaiMonId).HasColumnName("LoaiMonID");

                entity.Property(e => e.TenMon).HasMaxLength(50);

                entity.HasOne(d => d.CongThuc)
                    .WithMany(p => p.Mons)
                    .HasForeignKey(d => d.CongThucId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Mon__CongThucID__30F848ED");

                entity.HasOne(d => d.LoaiMon)
                    .WithMany(p => p.Mons)
                    .HasForeignKey(d => d.LoaiMonId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Mon__LoaiMonID__300424B4");
            });

            modelBuilder.Entity<MonDonDatMon>(entity =>
            {
                entity.HasKey(e => new { e.MonId, e.DonDatMonId })
                    .HasName("PK__Mon_DonD__BDD713DCE43CA309");

                entity.ToTable("Mon_DonDatMon");

                entity.Property(e => e.MonId).HasColumnName("MonID");

                entity.Property(e => e.DonDatMonId).HasColumnName("DonDatMonID");

                entity.HasOne(d => d.DonDatMon)
                    .WithMany(p => p.MonDonDatMons)
                    .HasForeignKey(d => d.DonDatMonId)
                    .HasConstraintName("FK__Mon_DonDa__DonDa__5165187F");

                entity.HasOne(d => d.Mon)
                    .WithMany(p => p.MonDonDatMons)
                    .HasForeignKey(d => d.MonId)
                    .HasConstraintName("FK__Mon_DonDa__MonID__5070F446");
            });

            modelBuilder.Entity<NguyenLieu>(entity =>
            {
                entity.ToTable("NguyenLieu");

                entity.Property(e => e.NguyenLieuId).HasColumnName("NguyenLieuID");

                entity.Property(e => e.DonViTinh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gia).HasColumnType("money");

                entity.Property(e => e.NhaCungCapId).HasColumnName("NhaCungCapID");

                entity.Property(e => e.TenNguyenLieu).HasMaxLength(50);

                entity.HasOne(d => d.NhaCungCap)
                    .WithMany(p => p.NguyenLieus)
                    .HasForeignKey(d => d.NhaCungCapId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__NguyenLie__NhaCu__33D4B598");
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.ToTable("NhaCungCap");

                entity.Property(e => e.NhaCungCapId).HasColumnName("NhaCungCapID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNhaCungCap).HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.ToTable("NhanVien");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.CaId).HasColumnName("CaID");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.LoaiNv).HasColumnName("LoaiNV");

                entity.Property(e => e.NhanVienQl).HasColumnName("NhanVienQL");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNhanVien).HasMaxLength(50);

                entity.HasOne(d => d.Ca)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.CaId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__NhanVien__CaID__3C69FB99");

                entity.HasOne(d => d.LoaiNvNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.LoaiNv)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__NhanVien__LoaiNV__3A81B327");

                entity.HasOne(d => d.NhanVienQlNavigation)
                    .WithMany(p => p.InverseNhanVienQlNavigation)
                    .HasForeignKey(d => d.NhanVienQl)
                    .HasConstraintName("FK__NhanVien__NhanVi__3B75D760");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.TaiKhoan1)
                    .HasName("PK__TaiKhoan__D5B8C7F1797BF08F");

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.TaiKhoan1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TaiKhoan")
                    .IsFixedLength();

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__TaiKhoan__NhanVi__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

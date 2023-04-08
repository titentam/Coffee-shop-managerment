using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PBL3.Model
{
    public partial class PBL3Context : DbContext
    {
        public PBL3Context()
        {
        }

        public PBL3Context(DbContextOptions<PBL3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Ban> Bans { get; set; } = null!;
        public virtual DbSet<CaLamViec> CaLamViecs { get; set; } = null!;
        public virtual DbSet<ChiTietDonDatMon> ChiTietDonDatMons { get; set; } = null!;
        public virtual DbSet<CongThuc> CongThucs { get; set; } = null!;
        public virtual DbSet<CtNl> CtNls { get; set; } = null!;
        public virtual DbSet<DonDatMon> DonDatMons { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<LoaiMon> LoaiMons { get; set; } = null!;
        public virtual DbSet<LoaiNv> LoaiNvs { get; set; } = null!;
        public virtual DbSet<Mon> Mons { get; set; } = null!;
        public virtual DbSet<NguyenLieu> NguyenLieus { get; set; } = null!;
        public virtual DbSet<NhNl> NhNls { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<NhapHang> NhapHangs { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=PBL3;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ban>(entity =>
            {
                entity.HasKey(e => e.MaBan);

                entity.ToTable("Ban");

                entity.Property(e => e.MaBan)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ban1).HasColumnName("Ban");
            });

            modelBuilder.Entity<CaLamViec>(entity =>
            {
                entity.HasKey(e => e.MaCa);

                entity.ToTable("CaLamViec");

                entity.Property(e => e.MaCa).ValueGeneratedNever();

                entity.Property(e => e.TenCa).HasMaxLength(100);

                entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChiTietDonDatMon>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ChiTietDonDatMon");

                entity.Property(e => e.MaDonDatMon)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MaMon)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaDonDatMonNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaDonDatMon)
                    .HasConstraintName("FK__ChiTietDo__MaDon__0697FACD");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaMon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDo__MaMon__078C1F06");
            });

            modelBuilder.Entity<CongThuc>(entity =>
            {
                entity.HasKey(e => e.MaCongThuc);

                entity.ToTable("CongThuc");

                entity.Property(e => e.MaCongThuc)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CongThuc1)
                    .HasMaxLength(255)
                    .HasColumnName("CongThuc");

                entity.Property(e => e.TenCongThuc).HasMaxLength(255);
            });

            modelBuilder.Entity<CtNl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CT-NL");

                entity.Property(e => e.MaCongThuc)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MaNguyenLieu)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaCongThucNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaCongThuc)
                    .HasConstraintName("FK__CT-NL__MaCongThu__72910220");

                entity.HasOne(d => d.MaNguyenLieuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaNguyenLieu)
                    .HasConstraintName("FK__CT-NL__MaNguyenL__6CD828CA");
            });

            modelBuilder.Entity<DonDatMon>(entity =>
            {
                entity.HasKey(e => e.MaDonDatMon);

                entity.ToTable("DonDatMon");

                entity.Property(e => e.MaDonDatMon)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.GhiChu).HasMaxLength(200);

                entity.Property(e => e.MaBan)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MaHd)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaHD");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaNV");

                entity.HasOne(d => d.MaBanNavigation)
                    .WithMany(p => p.DonDatMons)
                    .HasForeignKey(d => d.MaBan)
                    .HasConstraintName("FK__DonDatMon__MaBan__6FE99F9F");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.DonDatMons)
                    .HasForeignKey(d => d.MaHd)
                    .HasConstraintName("FK__DonDatMon__MaHD__0D7A0286");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.DonDatMons)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK__DonDatMon__MaNV__0B91BA14");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaHD");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaNV");

                entity.Property(e => e.ThoiGianTao).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.Property(e => e.Vat)
                    .HasMaxLength(20)
                    .HasColumnName("VAT");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaNv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDon__MaNV__0E6E26BF");
            });

            modelBuilder.Entity<LoaiMon>(entity =>
            {
                entity.HasKey(e => e.TenLoai)
                    .HasName("PK_LoaiMon_1");

                entity.ToTable("LoaiMon");

                entity.Property(e => e.TenLoai).HasMaxLength(100);

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoaiNv>(entity =>
            {
                entity.HasKey(e => e.LoaiNv1)
                    .HasName("PK_LoaiNV_1");

                entity.ToTable("LoaiNV");

                entity.Property(e => e.LoaiNv1)
                    .HasMaxLength(100)
                    .HasColumnName("LoaiNV");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mon>(entity =>
            {
                entity.HasKey(e => e.MaMon);

                entity.ToTable("Mon");

                entity.Property(e => e.MaMon)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Gia).HasColumnType("money");

                entity.Property(e => e.Loai).HasMaxLength(100);

                entity.Property(e => e.MaCongThuc)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ten).HasMaxLength(100);

                entity.HasOne(d => d.LoaiNavigation)
                    .WithMany(p => p.Mons)
                    .HasForeignKey(d => d.Loai)
                    .HasConstraintName("FK__Mon__Loai__0880433F");

                entity.HasOne(d => d.MaCongThucNavigation)
                    .WithMany(p => p.Mons)
                    .HasForeignKey(d => d.MaCongThuc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mon__MaCongThuc__73852659");
            });

            modelBuilder.Entity<NguyenLieu>(entity =>
            {
                entity.HasKey(e => e.MaNguyenLieu);

                entity.ToTable("NguyenLieu");

                entity.Property(e => e.MaNguyenLieu)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DonGia).HasColumnType("money");

                entity.Property(e => e.Dvt)
                    .HasMaxLength(255)
                    .HasColumnName("DVT");

                entity.Property(e => e.MaNhaCc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaNhaCC");

                entity.Property(e => e.TenNguyenLieu).HasMaxLength(255);

                entity.HasOne(d => d.MaNhaCcNavigation)
                    .WithMany(p => p.NguyenLieus)
                    .HasForeignKey(d => d.MaNhaCc)
                    .HasConstraintName("FK__NguyenLie__MaNha__65370702");
            });

            modelBuilder.Entity<NhNl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NH-NL");

                entity.Property(e => e.MaDonHang)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MaNguyenLieu)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TongGia).HasColumnType("money");

                entity.HasOne(d => d.MaDonHangNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaDonHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NH-NL__MaDonHang__671F4F74");

                entity.HasOne(d => d.MaNguyenLieuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaNguyenLieu)
                    .HasConstraintName("FK__NH-NL__MaNguyenL__634EBE90");
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNhaCc);

                entity.ToTable("NhaCungCap");

                entity.Property(e => e.MaNhaCc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaNhaCC");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(255)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNhaCc)
                    .HasMaxLength(255)
                    .HasColumnName("TenNhaCC");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.ToTable("NhanVien");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaNV");

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.HoTen).HasMaxLength(100);

                entity.Property(e => e.LoaiNv)
                    .HasMaxLength(100)
                    .HasColumnName("LoaiNV");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(100)
                    .HasColumnName("SDT");

                entity.HasOne(d => d.LoaiNvNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.LoaiNv)
                    .HasConstraintName("FK__NhanVien__LoaiNV__0A9D95DB");

                entity.HasOne(d => d.MaCaNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.MaCa)
                    .HasConstraintName("FK__NhanVien__MaCa__09A971A2");
            });

            modelBuilder.Entity<NhapHang>(entity =>
            {
                entity.HasKey(e => e.MaDonHang);

                entity.ToTable("NhapHang");

                entity.Property(e => e.MaDonHang)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.Property(e => e.MaNv)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaNV");

                entity.Property(e => e.NgayDatHang).HasColumnType("date");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.NhapHangs)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK__NhapHang__MaNV__5224328E");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MaNV");

                entity.Property(e => e.MatKhau).HasMaxLength(100);

                entity.Property(e => e.TaiKhoan1)
                    .HasMaxLength(100)
                    .HasColumnName("TaiKhoan");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK__TaiKhoan__MaNV__08B54D69");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

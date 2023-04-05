create database Tamtentoi
go
use Tamtentoi
go


CREATE TABLE LoaiNhanVien
(
  LoaiNV INT IDENTITY(1,1) Not null,
  TenLoai nvarchar(50) ,
  PRIMARY KEY (LoaiNV)
);

CREATE TABLE Ban
(
  BanID INT IDENTITY(1,1) Not null,
  TinhTrang BIT ,
  PRIMARY KEY (BanID)
);

CREATE TABLE LoaiMon
(
  LoaiMonID INT IDENTITY(1,1) Not null,
  TenLoaiMon nvarchar(50) ,
  PRIMARY KEY (LoaiMonID)
);

CREATE TABLE CongThuc
(
  CongThucID INT IDENTITY(1,1) Not null,
  TenCongThuc nvarchar(50) ,
  PRIMARY KEY (CongThucID)
);

CREATE TABLE NhaCungCap
(
  NhaCungCapID INT IDENTITY(1,1) Not null,
  TenNhaCungCap nvarchar(50) ,
  SDT VARCHAR(20) ,
  Email VARCHAR(50) ,
  PRIMARY KEY (NhaCungCapID)
);

CREATE TABLE CaLam
(
  CaID INT IDENTITY(1,1) Not null,
  TenCa nvarchar(50) ,
  ThoiGianBatDau Datetime ,
  ThoiGianKetThuc Datetime ,
  PRIMARY KEY (CaID)
);

CREATE TABLE Mon
(
  MonID INT IDENTITY(1,1) Not null,
  TenMon nvarchar(50) ,
  Gia Money ,
  LoaiMonID INT ,
  CongThucID INT ,
  PRIMARY KEY (MonID),
  FOREIGN KEY (LoaiMonID) REFERENCES LoaiMon(LoaiMonID)
  on delete set null
  on update CASCADE,
  FOREIGN KEY (CongThucID) REFERENCES CongThuc(CongThucID)
  on delete set null
  on update CASCADE
);

CREATE TABLE NguyenLieu
(
  NguyenLieuID INT IDENTITY(1,1) Not null,
  TenNguyenLieu nvarchar(50) ,
  SoLuong INT ,
  Gia Money ,
  DonViTinh VARCHAR(50) ,
  NhaCungCapID INT ,
  PRIMARY KEY (NguyenLieuID),
  FOREIGN KEY (NhaCungCapID) REFERENCES NhaCungCap(NhaCungCapID)
  on delete set null
  on update CASCADE
);

CREATE TABLE CongThuc_NguyenLieu
(
  SoLuong INT ,
  NguyenLieuID INT Not null,
  CongThucID INT Not null,
  PRIMARY KEY (NguyenLieuID, CongThucID),
  FOREIGN KEY (NguyenLieuID) REFERENCES NguyenLieu(NguyenLieuID)
  on delete cascade
  on update CASCADE,
  FOREIGN KEY (CongThucID) REFERENCES CongThuc(CongThucID)
  on delete cascade
  on update CASCADE
);

CREATE TABLE NhanVien
(
  NhanVienID INT IDENTITY(1,1) Not null,
  TenNhanVien nvarchar(50) ,
  SDT VARCHAR(20) ,
  DiaChi nvarchar(50) ,
  LoaiNV INT ,
  NhanVienQL INT ,
  CaID INT ,
  PRIMARY KEY (NhanVienID),
  FOREIGN KEY (LoaiNV) REFERENCES LoaiNhanVien(LoaiNV)
  on delete set null
  on update CASCADE,
  FOREIGN KEY (NhanVienQL) REFERENCES NhanVien(NhanVienID),
  FOREIGN KEY (CaID) REFERENCES CaLam(CaID)
  on delete set null
  on update CASCADE
);

CREATE TABLE TaiKhoan
(
  TaiKhoan CHAR(15) Not null,
  MatKhau VARCHAR(50) ,
  NhanVienID INT ,
  PRIMARY KEY (TaiKhoan),
  FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
  on delete cascade
  on update CASCADE
);

CREATE TABLE HoaDon
(
  HoaDonID INT IDENTITY(1,1) Not null,
  ThoiGianTao Datetime ,
  TongTien Money ,
  TrangThaiThanhToan BIT ,
  NhanVienID INT ,
  PRIMARY KEY (HoaDonID),
  FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
  on delete set null
  on update CASCADE
);

CREATE TABLE DonDatMon
(
  DonDatMonID INT IDENTITY(1,1) Not null,
  GhiChu nvarchar(255) ,
  HoaDonID INT ,
  BanID INT ,
  NhanVienID INT ,
  PRIMARY KEY (DonDatMonID),
  FOREIGN KEY (HoaDonID) REFERENCES HoaDon(HoaDonID)
  on delete set null
  ,
  FOREIGN KEY (BanID) REFERENCES Ban(BanID)
  on delete set null
  on update CASCADE,
  FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
  on delete set null
  on update CASCADE
);

CREATE TABLE DatHang
(
  DathangID INT IDENTITY(1,1) Not null,
  NgayDat datetime ,
  GhiChu nvarchar(255) ,
  NhanVienID INT ,
  PRIMARY KEY (DathangID),
  FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
  on delete set null
  on update CASCADE
);

CREATE TABLE DatHang_NguyenLieu
(
  SoLuong INT ,
  TongGia Money ,
  NguyenLieuID INT Not null,
  DathangID INT Not null,
  PRIMARY KEY (NguyenLieuID, DathangID),
  FOREIGN KEY (NguyenLieuID) REFERENCES NguyenLieu(NguyenLieuID)
  on delete cascade
  on update CASCADE,
  FOREIGN KEY (DathangID) REFERENCES DatHang(DathangID)
  on delete cascade
  on update CASCADE
);

CREATE TABLE Mon_DonDatMon
(
  SoLuong INT ,
  MonID INT Not null,
  DonDatMonID INT Not null,
  PRIMARY KEY (MonID, DonDatMonID),
  FOREIGN KEY (MonID) REFERENCES Mon(MonID)
  on delete cascade
  on update CASCADE,
  FOREIGN KEY (DonDatMonID) REFERENCES DonDatMon(DonDatMonID)
  on delete cascade
  on update CASCADE
);

-- insert data
INSERT INTO LoaiNhanVien (TenLoai) VALUES
(N'Quản lý'),
(N'Phục vụ'),
(N'Pha chế');

INSERT INTO Ban (TinhTrang) VALUES
(0),
(0),
(0),
(0),
(0),
(0),
(0),
(0),
(0),
(0);

INSERT INTO LoaiMon (TenLoaiMon) VALUES
(N'Cà phê'),
(N'Trà sữa'),
(N'Bánh ngọt'),
(N'Trà'),
(N'Kem');

INSERT INTO CongThuc (TenCongThuc) VALUES
(N'Cà phê đen'),
(N'Cà phê sữa'),
(N'Bánh milo'),
(N'Trà đào'),
(N'Trà sen'),
(N'Trà xanh');

INSERT INTO NhaCungCap (TenNhaCungCap, SDT, Email) VALUES
(N'Công ty TNHH Sản xuất và Thương mại ABC', '0901234567', 'abc@gmail.com'),
(N'Công ty TNHH Sản xuất và Thương mại XYZ', '0909876543', 'xyz@gmail.com'),
(N'Công ty TNHH Sản xuất và Thương mại DEF', '0901112222', 'def@gmail.com');

INSERT INTO CaLam (TenCa, ThoiGianBatDau, ThoiGianKetThuc) VALUES
(N'Ca sáng', '07:00:00', '12:00:00'),
(N'Ca chiều', '12:00:00', '17:00:00'),
(N'Ca tối', '17:00:00', '22:00:00');

INSERT INTO Mon (TenMon, Gia, LoaiMonID, CongThucID) VALUES
(N'Cà phê đen', 25000, 1, 1),
(N'Cà phê sữa', 30000, 1, 2),
(N'Trà đào', 35000, 4, 3),
(N'Trà sen', 40000, 4, 4),
(N'Trà xanh', 45000, 4, 5),
(N'Bánh mì thịt nướng', 35000, 3, NULL),
(N'Rau câu', 20000, 3, NULL),
(N'Kem', 35000, 5, NULL);

INSERT INTO NguyenLieu (TenNguyenLieu, SoLuong, Gia, DonViTinh, NhaCungCapID) VALUES
(N'Cà phê', 100, 200000, 'gram', 1),
(N'Trà đào', 50, 300000, 'chai', 2),
(N'Nước ép táo', 30, 150000, 'lit', 3),
(N'Bánh mì', 50, 10000, 'cai', NULL),
(N'Sữa', 40, 25000, 'lit', NULL),
(N'Kem', 20, 35000, 'kg', NULL);

INSERT INTO CongThuc_NguyenLieu (SoLuong, NguyenLieuID, CongThucID) VALUES
(10, 1, 1),
(20, 6, 6),
(5, 3, 1),
(10, 3, 2),
(10, 1, 6),
(20, 4, 2),
(10, 5, 2),
(5, 1, 3),
(10, 2, 3),
(5, 3, 3),
(10, 4, 3),
(5, 5, 3),
(10, 2, 5),
(5, 1, 4),
(10, 2, 4),
(5, 4, 4);

INSERT INTO NhanVien (TenNhanVien, DiaChi, SDT, LoaiNV,NhanVienQL, CaID) VALUES
(N'Nguyễn Văn A', N'Hà Nội', '0987654321', 1, null, 1),
(N'Phạm Thị B', N'Hải Phòng', '0123456789', 2, 1,2),
(N'Trần Văn C', N'Hà Nội', '0987654321', 2,1,  3),
(N'Lê Thị D', N'Hà Nội', '0123456789', 3, 1, 1);

INSERT INTO HoaDon (ThoiGianTao, TongTien, NhanVienID) VALUES
('2023-04-01', 350000, 2),
('2023-04-02', 450000, 2),
('2023-04-02', 560000, 3),
('2023-04-03', 250000, 3);

INSERT INTO DonDatMon (GhiChu, HoaDonID, BanID, NhanVienID) VALUES 
(N'Đặt món trước 10 phút', 1, 2, 2);

INSERT INTO DatHang (NgayDat, GhiChu, NhanVienID) VALUES 
('2023-04-03 09:00:00', N'Đặt hàng nguyên liệu cho tuần này', 1),
('2023-04-03 09:00:00', N'Đặt hàng nguyên liệu cho tuần sau', 2);

INSERT INTO DatHang_NguyenLieu (SoLuong, TongGia, NguyenLieuID, DathangID) VALUES 
(50, 200000, 1, 1),
(70, 200000, 3, 1),
(50, 250000, 2, 2);

INSERT INTO Mon_DonDatMon (SoLuong, MonID, DonDatMonID) VALUES 
(2, 1, 1),
(5, 3, 1);

INSERT INTO TaiKhoan (TaiKhoan, MatKhau, NhanVienID) VALUES 
('admin', '123456', 1),
('server', '123456', 2),
('bartender', '123456', 3),
('tam', '123456', 4);

select * from Mon_DonDatMon
select * from DatHang_NguyenLieu
select * from NguyenLieu
select * from CongThuc_NguyenLieu


TRUNCATE TABLE Ban;
TRUNCATE TABLE calam;
TRUNCATE TABLE congthuc;
TRUNCATE TABLE congthuc_nguyenlieu;
TRUNCATE TABLE dathang;
TRUNCATE TABLE dathang_nguyenlieu;
TRUNCATE TABLE dondatmon;
TRUNCATE TABLE hoadon;
TRUNCATE TABLE loaimon;
TRUNCATE TABLE loainhanvien;
TRUNCATE TABLE mon;
TRUNCATE TABLE mon_dondatmon;
TRUNCATE TABLE nguyenlieu;
TRUNCATE TABLE nhacungcap;
TRUNCATE TABLE nhanvien;
TRUNCATE TABLE taikhoan;



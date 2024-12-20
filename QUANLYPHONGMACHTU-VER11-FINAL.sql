USE [master]
GO
CREATE DATABASE [QUANLYPHONGMACHTU]
GO
USE [QUANLYPHONGMACHTU]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BACSI](
	[MaBS] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [nvarchar](3) NULL,
	[Email] [nvarchar](50) NULL,
	[SDT] [nvarchar](10) NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[NgayVaoLam] [date] NOT NULL,
	[BangCap] [nvarchar](50) NULL,
	[Image] [varbinary](max) NULL,
 CONSTRAINT [PK_BACSI] PRIMARY KEY CLUSTERED 
(
	[MaBS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BAOCAODOANHTHU](
	[MaBCDT] [int] IDENTITY(1,1) NOT NULL,
	[Thang] [int] NULL,
	[Nam] [int] NULL,
	[TongDoanhThu] [float] NULL,
 CONSTRAINT [PK_BAOCAODOANHTHU] PRIMARY KEY CLUSTERED 
(
	[MaBCDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BAOCAOSUDUNGTHUOC](
	[MaBaoCaoSuDungThuoc] [int] IDENTITY(1,1) NOT NULL,
	[Thang] [int] NULL,
	[SoLanDung] [int] NULL,
	[TongSoLuongDaDung] [int] NULL,
	[MaThuoc] [int] NULL,
 CONSTRAINT [PK_BAOCAOSUDUNGTHUOC] PRIMARY KEY CLUSTERED 
(
	[MaBaoCaoSuDungThuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [BENHNHAN](
	[MaBN] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[GioiTinh] [nvarchar](10) NOT NULL,
	[NamSinh] [date] NOT NULL,
	[DiaChi] [nvarchar](50) NULL,
	[TrangThai] [bit] NULL,
 CONSTRAINT [PK_BENHNHAN] PRIMARY KEY CLUSTERED 
(
	[MaBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CACHDUNG](
	[MaCachDung] [int] IDENTITY(1,1) NOT NULL,
	[TenCachDung] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CACHDUNG] PRIMARY KEY CLUSTERED 
(
	[MaCachDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CHUCNANG](
	[MaChucNang] [nvarchar](10) NOT NULL,
	[TenChucNang] [nvarchar](200) NULL,
	[TenManHinhDuocLoad] [nvarchar](100) NULL,
 CONSTRAINT [PK_CHUCNANG] PRIMARY KEY CLUSTERED 
(
	[MaChucNang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CT_BCDT](
	[CTBCDT_ID] [int] IDENTITY(1,1) NOT NULL,
	[MaBCDT] [int] NOT NULL,
	[SoHD] [int] NOT NULL,
	[SoBenhNhan] [int] NULL,
	[DoanhThu] [float] NULL,
	[TiLe] [float] NULL,
 CONSTRAINT [PK_CT_BCDT] PRIMARY KEY CLUSTERED 
(
	[CTBCDT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CT_PKB](
	[MaPKB] [int] NOT NULL,
	[MaThuoc] [int] NOT NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_CT_PKB] PRIMARY KEY CLUSTERED 
(
	[MaPKB] ASC,
	[MaThuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [CT_PNT](
	[CTPNT_ID] [int] NOT NULL,
	[SoPhieuNhap] [int] NOT NULL,
	[MaThuoc] [int] NULL,
	[SoLuongNhap] [int] NULL,
	[DonGiaNhap] [float] NULL,
	[ThanhTien] [float] NULL,
 CONSTRAINT [PK_CT_PNT] PRIMARY KEY CLUSTERED 
(
	[CTPNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DVT](
	[MaDVT] [int] IDENTITY(1,1) NOT NULL,
	[TenDVT] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DVT] PRIMARY KEY CLUSTERED 
(
	[MaDVT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HOADON](
	[SoHD] [int] IDENTITY(1,1) NOT NULL,
	[TienKham] [float] NOT NULL,
	[TienThuoc] [float] NULL,
	[TongTien] [float] NOT NULL,
	[MaBN] [int] NULL,
	[MaPKB] [int] NULL,
	[TrangThai] [nvarchar](50) NULL,
 CONSTRAINT [PK_HOADON] PRIMARY KEY CLUSTERED 
(
	[SoHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [LOAIBENH](
	[MaLoaiBenh] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiBenh] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LOAIBENH] PRIMARY KEY CLUSTERED 
(
	[MaLoaiBenh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [LOAITHUOC](
	[MaLoaiThuoc] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiThuoc] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LOAITHUOC] PRIMARY KEY CLUSTERED 
(
	[MaLoaiThuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [NGUOIDUNG](
	[TenDangNhap] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](50) NULL,
	[MaNhom] [int] NULL,
	[MaBS] [int] NULL,
 CONSTRAINT [PK_NGUOIDUNG] PRIMARY KEY CLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [NHOMNGUOIDUNG](
	[MaNhom] [int] IDENTITY(1,1) NOT NULL,
	[TenNhom] [nvarchar](50) NULL,
 CONSTRAINT [PK_NHOMNGUOIDUNG] PRIMARY KEY CLUSTERED 
(
	[MaNhom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PHANQUYEN](
	[PHANQUYEN_ID] [int] NOT NULL,
	[MaNhom] [int] NOT NULL,
	[MaChucNang] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_PHANQUYEN] PRIMARY KEY CLUSTERED 
(
	[PHANQUYEN_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PHIEUKHAMBENH](
	[MaPKB] [int] IDENTITY(1,1) NOT NULL,
	[NgayKham] [date] NOT NULL,
	[MaLoaiBenh] [int] NOT NULL,
	[MaBS] [int] NULL,
	[TrieuChung] [nvarchar](200) NULL,
	[MaBN] [int] NULL,
 CONSTRAINT [PK_PHIEUKHAMBENH] PRIMARY KEY CLUSTERED 
(
	[MaPKB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PHIEUNHAPTHUOC](
	[SoPhieuNhap] [int] IDENTITY(1,1) NOT NULL,
	[NgayNhap] [datetime] NULL,
	[TongTien] [float] NOT NULL,
 CONSTRAINT [PK_PHIEUNHAPTHUOC] PRIMARY KEY CLUSTERED 
(
	[SoPhieuNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [THAMSO](
	[MaThamSo] [int] IDENTITY(1,1) NOT NULL,
	[TenThamSo] [nvarchar](100) NOT NULL,
	[GiaTri] [float] NOT NULL,
 CONSTRAINT [PK_THAMSO] PRIMARY KEY CLUSTERED 
(
	[MaThamSo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [THUOC](
	[MaThuoc] [int] IDENTITY(1,1) NOT NULL,
	[MaLoaiThuoc] [int] NOT NULL,
	[TenThuoc] [nvarchar](50) NOT NULL,
	[MaDVT] [int] NOT NULL,
	[MaCachDung] [int] NOT NULL,
	[DonGiaNhap] [float] NULL,
	[DonGiaBan] [float] NULL,
	[SoLuong] [int] NULL,
 CONSTRAINT [PK_THUOC] PRIMARY KEY CLUSTERED 
(
	[MaThuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [BACSI] ON 

INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (1, N'Nguyễn Đại Dương', CAST(N'1980-06-24' AS Date), N'Nam', N'abc@gmail.com', N'0112456789', N'123 Luxubu TanPhu', CAST(N'2016-05-18' AS Date), N'Bác sĩ cơ bản', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (3, N'Nguyễn Văn Bác', CAST(N'1980-05-19' AS Date), N'Nam', N'nguyenvanbac@example.com', N'0123456789', N'Hà Nội', CAST(N'2010-05-20' AS Date), N'Bác sĩ cơ bản', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (4, N'Trần Thị Thu', CAST(N'1985-07-20' AS Date), N'Nữ ', N'tranthithu@example.com', N'0987654321', N'Hồ Chí Minh', CAST(N'2012-08-10' AS Date), N'Bác sĩ chuyên khoa', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (5, N'Lê Văn Tùng', CAST(N'1973-03-10' AS Date), N'Nam', N'levantung@example.com', N'0369871234', N'Đà Nẵng', CAST(N'2005-12-05' AS Date), N'Bác sĩ cơ bản', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (6, N'Phạm Thị Thanh', CAST(N'1988-11-25' AS Date), N'Nữ ', N'phamthithanh@example.com', N'0975328641', N'Hải Phòng', CAST(N'2015-04-30' AS Date), N'Bác sĩ cơ bản', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (7, N'Huỳnh Văn Quốc', CAST(N'1970-09-08' AS Date), N'Nam', N'huynhvanquoc@example.com', N'0358741963', N'Cần Thơ', CAST(N'2002-10-15' AS Date), N'Bác sĩ chuyên khoa', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (8, N'Võ Thị Mai', CAST(N'1983-05-18' AS Date), N'Nữ ', N'vothimai@example.com', N'0912345678', N'Bình Dương', CAST(N'2008-07-20' AS Date), N'Bác sĩ cơ bản', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (9, N'Đặng Văn Phong', CAST(N'1975-11-30' AS Date), N'Nam', N'dangvanphong@example.com', N'0569871234', N'Đồng Nai', CAST(N'2004-06-10' AS Date), N'Bác sĩ cơ bản', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (10, N'Trần Văn Minh', CAST(N'1968-09-05' AS Date), N'Nam', N'tranvanminh@example.com', N'0987456321', N'Hải Dương', CAST(N'2000-03-25' AS Date), N'Bác sĩ chuyên khoa', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (11, N'Nguyễn Thị Hương', CAST(N'1980-04-12' AS Date), N'Nữ ', N'nguyenthihuong@example.com', N'0912876543', N'Hưng Yên', CAST(N'2011-09-18' AS Date), N'Bác sĩ cơ bản', NULL)
INSERT [BACSI] ([MaBS], [HoTen], [NgaySinh], [GioiTinh], [Email], [SDT], [DiaChi], [NgayVaoLam], [BangCap], [Image]) VALUES (12, N'Phan Văn Tâm', CAST(N'1977-06-22' AS Date), N'Nam', N'phanvantam@example.com', N'0362547891', N'Hà Tĩnh', CAST(N'2006-11-08' AS Date), N'Bác sĩ chuyên khoa', NULL)
SET IDENTITY_INSERT [BACSI] OFF
GO
SET IDENTITY_INSERT [BAOCAODOANHTHU] ON 

INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (7, 1, 2024, 1150000)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (9, 2, 2024, 780000)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (10, 3, 2024, NULL)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (11, 4, 2024, NULL)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (12, 5, 2024, NULL)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (13, 1, 2024, 1360000)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (14, 2, 2024, 780000)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (15, 3, 2024, 630000)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (16, 4, 2024, 940000)
INSERT [BAOCAODOANHTHU] ([MaBCDT], [Thang], [Nam], [TongDoanhThu]) VALUES (17, 5, 2024, 860000)
SET IDENTITY_INSERT [BAOCAODOANHTHU] OFF
GO
SET IDENTITY_INSERT [BAOCAOSUDUNGTHUOC] ON 

INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (8, 1, 3, 9, 1)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (9, 1, 1, 1, 3)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (10, 1, 1, 2, 4)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (11, 4, 1, 2, 4)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (12, 1, 1, 1, 5)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (13, 3, 1, 1, 5)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (14, 1, 1, 1, 7)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (15, 4, 1, 1, 7)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (16, 1, 1, 2, 8)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (17, 4, 1, 2, 8)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (18, 1, 2, 4, 9)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (19, 4, 1, 1, 9)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (20, 4, 1, 2, 10)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (21, 5, 1, 2, 12)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (22, 5, 1, 1, 13)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (23, 5, 1, 2, 14)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (24, 5, 1, 1, 15)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (25, 5, 1, 2, 16)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (26, 2, 1, 3, 17)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (27, 2, 1, 2, 18)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (28, 3, 1, 1, 19)
INSERT [BAOCAOSUDUNGTHUOC] ([MaBaoCaoSuDungThuoc], [Thang], [SoLanDung], [TongSoLuongDaDung], [MaThuoc]) VALUES (29, 3, 1, 2, 20)
SET IDENTITY_INSERT [BAOCAOSUDUNGTHUOC] OFF
GO
SET IDENTITY_INSERT [BENHNHAN] ON 

INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (1, N'Nguyen Van A', N'Nam', CAST(N'1980-01-01' AS Date), N'Ha Noi', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (2, N'Tran Thi B', N'Nữ', CAST(N'1990-02-02' AS Date), N'Ho Chi Minh', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (3, N'Le Van C', N'Nam', CAST(N'1985-03-03' AS Date), N'Da Nang', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (4, N'Pham Thi D', N'Nữ', CAST(N'1975-04-04' AS Date), N'Can Tho', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (5, N'Hoang Van E', N'Nam', CAST(N'2000-05-05' AS Date), N'Hai Phong', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (6, N'Vu Thi F', N'Nữ', CAST(N'1995-06-06' AS Date), N'Quang Ninh', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (7, N'Bui Van G', N'Nam', CAST(N'1988-07-07' AS Date), N'Hue', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (8, N'Do Thi H', N'Nữ', CAST(N'1979-08-08' AS Date), N'Nha Trang', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (9, N'Nguyen Van I', N'Nam', CAST(N'1992-09-09' AS Date), N'Da Lat', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (10, N'Pham Thi J', N'Nữ', CAST(N'1983-10-10' AS Date), N'Vung Tau', NULL)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (11, N'Nguyễn Đại Dương', N'Nam', CAST(N'2003-07-26' AS Date), N'Hồ Chí Minh', 0)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (12, N'Hồ Tiến Vũ Bình', N'Nam', CAST(N'2004-01-26' AS Date), N'Hồ Chí Minh', 0)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (13, N'Lương Văn Minh', N'Nam', CAST(N'2004-02-12' AS Date), N'Hồ Chí Minh', 0)
INSERT [BENHNHAN] ([MaBN], [HoTen], [GioiTinh], [NamSinh], [DiaChi], [TrangThai]) VALUES (14, N'Đỗ Trọng Tình', N'Nam', CAST(N'2002-11-24' AS Date), N'Hồ Chí Minh', 0)
SET IDENTITY_INSERT [BENHNHAN] OFF
GO
SET IDENTITY_INSERT [CACHDUNG] ON 

INSERT [CACHDUNG] ([MaCachDung], [TenCachDung]) VALUES (1, N'Uống trước khi ăn')
INSERT [CACHDUNG] ([MaCachDung], [TenCachDung]) VALUES (2, N'Uống sau khi ăn')
INSERT [CACHDUNG] ([MaCachDung], [TenCachDung]) VALUES (3, N'Ngày uống 3 lần')
INSERT [CACHDUNG] ([MaCachDung], [TenCachDung]) VALUES (4, N'Ngậm')
SET IDENTITY_INSERT [CACHDUNG] OFF
GO
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN01', N'Xem danh sách hoá đơn', N'Thanh toán')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN02', N'Mở thanh toán hoá đơn', N'Thanh toán')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN03', N'Thanh toán hoá đơn', N'Thanh toán')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN04', N'Xoá hoá đơn', N'Thanh toán')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN05', N'Hiển thị danh sách thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN06', N'Thêm thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN07', N'Sửa thông tin thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN08', N'Tính số lượng thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN09', N'Lưu thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN10', N'Tìm kiếm thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN11', N'Xem chi tiết thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN12', N'Chỉnh sửa qui định thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN13', N'Xoá quy định thuốc', N'Kho thuốc')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN14', N'Chỉnh sửa số lượng bệnh nhân tối đa', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN15', N'Tăng số lượng bệnh nhân tối đa', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN16', N'Giảm số lượng bệnh nhân tối đa', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN17', N'Tính số lượng bệnh nhân', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN18', N'Lưu số lượng bệnh nhân', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN19', N'Tìm kiếm bệnh nhân', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN20', N'Xoá bệnh nhân', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN21', N'Chỉnh sửa bệnh nhân', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN22', N'Thêm bệnh nhân', N'Quản lí tiếp đón')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN23', N'Thay đổi ảnh đại diện', N'Hồ sơ bác sĩ')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN24', N'Thay đổi mật khẩu', N'Hồ sơ bác sĩ')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN25', N'Xem thông tin bác sĩ', N'Hồ sơ bác sĩ')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN26', N'Thêm bác sĩ', N'Hồ sơ bác sĩ')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN27', N'Chỉnh sửa bác sĩ', N'Hồ sơ bác sĩ')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN28', N'Xoá bác sĩ', N'Hồ sơ bác sĩ')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN29', N'Thêm thuốc', N'Quản lí khám bệnh')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN30', N'Chỉnh sửa thuốc', N'Quản lí khám bệnh')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN31', N'Xoá thuốc', N'Quản lí khám bệnh')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN32', N'Lưu_QLKB', N'Quản lí khám bệnh')
INSERT [CHUCNANG] ([MaChucNang], [TenChucNang], [TenManHinhDuocLoad]) VALUES (N'CN33', N'Huỷ_QLKB', N'Quản lí khám bệnh')
GO
SET IDENTITY_INSERT [CT_BCDT] ON 

INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (76, 13, 17, 1, 330000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (77, 13, 18, 1, 70000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (78, 13, 19, 1, 90000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (79, 13, 20, 1, 110000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (80, 13, 21, 1, 110000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (81, 13, 22, 1, 250000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (82, 13, 23, 1, 130000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (83, 13, 24, 1, 80000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (84, 13, 25, 1, 140000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (85, 13, 26, 1, 50000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (86, 14, 27, 1, 450000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (87, 14, 28, 1, 330000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (88, 15, 29, 1, 200000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (89, 15, 30, 1, 290000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (90, 15, 31, 1, 140000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (91, 16, 32, 1, 80000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (92, 16, 33, 1, 50000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (93, 16, 34, 1, 250000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (94, 16, 35, 1, 130000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (95, 16, 36, 1, 430000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (96, 17, 37, 1, 190000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (97, 17, 38, 1, 100000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (98, 17, 39, 1, 150000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (99, 17, 40, 1, 130000, 0)
INSERT [CT_BCDT] ([CTBCDT_ID], [MaBCDT], [SoHD], [SoBenhNhan], [DoanhThu], [TiLe]) VALUES (100, 17, 41, 1, 290000, 0)
SET IDENTITY_INSERT [CT_BCDT] OFF
GO
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (21, 9, 3)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (22, 1, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (23, 1, 3)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (24, 1, 4)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (25, 3, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (26, 8, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (27, 9, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (28, 4, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (29, 5, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (30, 7, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (43, 17, 3)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (44, 18, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (45, 19, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (46, 20, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (47, 5, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (48, 4, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (49, 7, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (50, 8, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (51, 9, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (52, 10, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (53, 12, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (54, 13, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (55, 14, 2)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (56, 15, 1)
INSERT [CT_PKB] ([MaPKB], [MaThuoc], [SoLuong]) VALUES (57, 16, 2)
GO
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (1, 2, 1, 10, 10000, 100000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (2, 2, 3, 20, 60000, 120000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (3, 2, 4, 4, 20000, 80000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (4, 3, 5, 4, 100000, 400000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (5, 3, 7, 2, 115000, 230000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (6, 3, 8, 5, 100000, 500000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (7, 5, 9, 5, 80000, 400000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (8, 6, 10, 1, 120000, 120000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (9, 6, 11, 1, 5000, 5000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (10, 7, 12, 10, 70000, 700000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (11, 7, 13, 10, 60000, 600000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (12, 7, 14, 4, 50000, 200000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (13, 8, 15, 5, 90000, 450000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (14, 9, 16, 5, 120000, 600000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (15, 9, 17, 4, 130000, 520000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (16, 9, 18, 4, 140000, 560000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (17, 10, 19, 10, 160000, 1600000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (18, 10, 20, 10, 120000, 1200000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (19, 11, 1, 10, 10000, 100000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (20, 11, 3, 10, 60000, 600000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (21, 11, 4, 10, 20000, 200000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (22, 12, 5, 5, 100000, 500000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (23, 12, 7, 5, 115000, 575000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (24, 12, 8, 1, 100000, 100000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (25, 13, 9, 1, 80000, 80000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (26, 13, 10, 1, 120000, 120000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (27, 14, 11, 20, 5000, 100000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (28, 14, 12, 30, 70000, 2100000)
INSERT [CT_PNT] ([CTPNT_ID], [SoPhieuNhap], [MaThuoc], [SoLuongNhap], [DonGiaNhap], [ThanhTien]) VALUES (29, 15, 13, 5, 60000, 300000)
GO
SET IDENTITY_INSERT [DVT] ON 

INSERT [DVT] ([MaDVT], [TenDVT]) VALUES (1, N'Viên')
INSERT [DVT] ([MaDVT], [TenDVT]) VALUES (2, N'Chai')
SET IDENTITY_INSERT [DVT] OFF
GO
SET IDENTITY_INSERT [HOADON] ON 

INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (17, 30000, 300000, 330000, 1, 21, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (18, 30000, 40000, 70000, 2, 22, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (19, 30000, 60000, 90000, 3, 23, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (20, 30000, 80000, 110000, 4, 24, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (21, 30000, 80000, 110000, 5, 25, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (22, 30000, 220000, 250000, 6, 26, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (23, 30000, 100000, 130000, 7, 27, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (24, 30000, 50000, 80000, 8, 28, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (25, 30000, 110000, 140000, 9, 29, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (26, 30000, 20000, 50000, 10, 30, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (27, 30000, 420000, 450000, 1, 43, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (28, 30000, 300000, 330000, 2, 44, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (29, 30000, 170000, 200000, 3, 45, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (30, 30000, 260000, 290000, 4, 46, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (31, 30000, 110000, 140000, 5, 47, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (32, 30000, 50000, 80000, 6, 48, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (33, 30000, 20000, 50000, 7, 49, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (34, 30000, 220000, 250000, 8, 50, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (35, 30000, 100000, 130000, 9, 51, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (36, 30000, 400000, 430000, 10, 52, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (37, 30000, 160000, 190000, 11, 53, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (38, 30000, 70000, 100000, 12, 54, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (39, 30000, 120000, 150000, 13, 55, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (40, 30000, 100000, 130000, 14, 56, NULL)
INSERT [HOADON] ([SoHD], [TienKham], [TienThuoc], [TongTien], [MaBN], [MaPKB], [TrangThai]) VALUES (41, 30000, 260000, 290000, 14, 57, NULL)
SET IDENTITY_INSERT [HOADON] OFF
GO
SET IDENTITY_INSERT [LOAIBENH] ON 

INSERT [LOAIBENH] ([MaLoaiBenh], [TenLoaiBenh]) VALUES (30, N'Bệnh đau dạ dày')
INSERT [LOAIBENH] ([MaLoaiBenh], [TenLoaiBenh]) VALUES (31, N'Viêm phổi')
INSERT [LOAIBENH] ([MaLoaiBenh], [TenLoaiBenh]) VALUES (32, N'Tiểu đường')
INSERT [LOAIBENH] ([MaLoaiBenh], [TenLoaiBenh]) VALUES (33, N'Viêm đường hô hấp trên ')
INSERT [LOAIBENH] ([MaLoaiBenh], [TenLoaiBenh]) VALUES (34, N'Bệnh tim mạch')
SET IDENTITY_INSERT [LOAIBENH] OFF
GO
SET IDENTITY_INSERT [LOAITHUOC] ON 

INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (1, N'Thuốc đau đầu')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (2, N'Thuốc hạ sốt')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (3, N'Thuốc giảm đau')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (4, N'Thuốc tiêu hoá')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (5, N'Thuốc kháng viêm')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (6, N'Thuốc kháng sinh')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (7, N'Thuốc kháng histamine')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (8, N'Thuốc điều trị tiểu đường')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (9, N'Thuốc điều trị hen suyễn')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (10, N'Thuốc điều trị hen phế quản')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (11, N'Thuốc giảm acid dạ dày')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (12, N'Thuốc điều trị tăng huyết áp')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (13, N'Thuốc giãn mạch')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (14, N'Thuốc điều trị viêm khớp')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (15, N'Thuốc điều trị viêm gan')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (16, N'Thuốc điều trị gout')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (17, N'Thuốc bổ sung canxi')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (18, N'Thuốc bổ sung sắt')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (19, N'Thuốc bổ dưỡng tổng hợp')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (20, N'Thuốc giảm cân')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (21, N'Thuốc trị vi khuẩn đường tiêu hóa')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (22, N'Thuốc trị viêm đại tràng')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (23, N'Thuốc điều trị viêm nhiễm phụ khoa')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (24, N'Thuốc chống co thắt đường ruột')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (25, N'Thuốc điều trị bệnh thận')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (26, N'Thuốc điều trị tăng lipid máu')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (27, N'Thuốc điều trị vấn đề tâm thần')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (28, N'Thuốc an thần')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (29, N'Thuốc điều trị bệnh gan')
INSERT [LOAITHUOC] ([MaLoaiThuoc], [TenLoaiThuoc]) VALUES (30, N'Thuốc điều trị bệnh về đường tiểu')
SET IDENTITY_INSERT [LOAITHUOC] OFF
GO
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'admin01', N'123456', 1, 1)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user01', N'123456', 2, 3)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user02', N'123456', 2, 4)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user03', N'123456', 2, 5)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user04', N'123456', 2, 6)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user05', N'123456', 2, 7)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user06', N'123456', 2, 8)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user07', N'123456', 2, 9)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user08', N'123456', 2, 10)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user09', N'123456', 2, 11)
INSERT [NGUOIDUNG] ([TenDangNhap], [MatKhau], [MaNhom], [MaBS]) VALUES (N'user10', N'123456', 2, 12)
GO
SET IDENTITY_INSERT [NHOMNGUOIDUNG] ON 

INSERT [NHOMNGUOIDUNG] ([MaNhom], [TenNhom]) VALUES (1, N'Chủ phòng khám')
INSERT [NHOMNGUOIDUNG] ([MaNhom], [TenNhom]) VALUES (2, N'Bác sĩ')
SET IDENTITY_INSERT [NHOMNGUOIDUNG] OFF
GO
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (1, 1, N'CN01')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (2, 1, N'CN11')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (3, 1, N'CN12')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (4, 1, N'CN13')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (5, 1, N'CN14')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (6, 1, N'CN15')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (7, 1, N'CN16')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (8, 1, N'CN17')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (9, 1, N'CN18')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (10, 1, N'CN26')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (11, 1, N'CN27')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (12, 1, N'CN28')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (13, 2, N'CN01')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (14, 2, N'CN02')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (15, 2, N'CN03')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (16, 2, N'CN04')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (17, 2, N'CN05')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (18, 2, N'CN06')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (19, 2, N'CN07')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (20, 2, N'CN08')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (21, 2, N'CN09')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (22, 2, N'CN10')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (23, 2, N'CN19')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (24, 2, N'CN20')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (25, 2, N'CN21')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (26, 2, N'CN22')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (27, 2, N'CN23')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (28, 2, N'CN24')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (29, 2, N'CN25')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (30, 2, N'CN29')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (31, 2, N'CN30')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (32, 2, N'CN31')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (33, 2, N'CN32')
INSERT [PHANQUYEN] ([PHANQUYEN_ID], [MaNhom], [MaChucNang]) VALUES (34, 2, N'CN33')
GO
SET IDENTITY_INSERT [PHIEUKHAMBENH] ON 

INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (21, CAST(N'2024-01-01' AS Date), 30, 1, N'Sốt cao', 1)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (22, CAST(N'2024-01-02' AS Date), 30, 1, N'Đau đầu', 2)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (23, CAST(N'2024-01-03' AS Date), 30, 1, N'Ho khan', 3)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (24, CAST(N'2024-01-04' AS Date), 31, 3, N'Đau bụng', 4)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (25, CAST(N'2024-01-05' AS Date), 31, 1, N'Mệt mỏi', 5)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (26, CAST(N'2024-01-06' AS Date), 32, 5, N'Sổ mũi', 6)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (27, CAST(N'2024-01-07' AS Date), 31, 1, N'Đau họng', 7)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (28, CAST(N'2024-01-08' AS Date), 33, 3, N'Nôn mửa', 8)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (29, CAST(N'2024-01-09' AS Date), 32, 6, N'Chóng mặt', 9)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (30, CAST(N'2024-01-10' AS Date), 31, 8, N'Đau ngực', 10)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (43, CAST(N'2024-02-15' AS Date), 30, 1, N'Ho', 1)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (44, CAST(N'2024-02-22' AS Date), 31, 5, N'Sốt', 2)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (45, CAST(N'2024-03-05' AS Date), 32, 3, N'Đau đầu', 3)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (46, CAST(N'2024-03-12' AS Date), 33, 4, N'Ho khan', 4)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (47, CAST(N'2024-03-19' AS Date), 34, 5, N'Tiêu chảy', 5)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (48, CAST(N'2024-04-02' AS Date), 30, 6, N'Ho', 6)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (49, CAST(N'2024-04-09' AS Date), 31, 7, N'Sốt', 7)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (50, CAST(N'2024-04-16' AS Date), 32, 8, N'Đau đầu', 8)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (51, CAST(N'2024-04-23' AS Date), 33, 9, N'Ho khan', 9)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (52, CAST(N'2024-04-30' AS Date), 34, 10, N'Tiêu chảy', 10)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (53, CAST(N'2024-05-07' AS Date), 30, 1, N'Ho', 11)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (54, CAST(N'2024-05-14' AS Date), 31, 8, N'Sốt', 12)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (55, CAST(N'2024-05-21' AS Date), 32, 3, N'Đau đầu', 13)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (56, CAST(N'2024-05-22' AS Date), 33, 4, N'Ho khan', 14)
INSERT [PHIEUKHAMBENH] ([MaPKB], [NgayKham], [MaLoaiBenh], [MaBS], [TrieuChung], [MaBN]) VALUES (57, CAST(N'2024-05-23' AS Date), 34, 5, N'Tiêu chảy', 14)
SET IDENTITY_INSERT [PHIEUKHAMBENH] OFF
GO
SET IDENTITY_INSERT [PHIEUNHAPTHUOC] ON 

INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (2, CAST(N'2024-05-14T00:00:00.000' AS DateTime), 300000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (3, CAST(N'2024-03-18T00:00:00.000' AS DateTime), 1130000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (5, CAST(N'2024-03-19T00:00:00.000' AS DateTime), 400000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (6, CAST(N'2024-04-01T00:00:00.000' AS DateTime), 125000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (7, CAST(N'2024-04-02T00:00:00.000' AS DateTime), 1500000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (8, CAST(N'2024-04-03T00:00:00.000' AS DateTime), 450000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (9, CAST(N'2024-04-04T00:00:00.000' AS DateTime), 1680000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (10, CAST(N'2024-04-05T00:00:00.000' AS DateTime), 2800000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (11, CAST(N'2024-04-06T00:00:00.000' AS DateTime), 900000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (12, CAST(N'2024-04-07T00:00:00.000' AS DateTime), 1175000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (13, CAST(N'2024-04-08T00:00:00.000' AS DateTime), 200000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (14, CAST(N'2024-04-09T00:00:00.000' AS DateTime), 2200000)
INSERT [PHIEUNHAPTHUOC] ([SoPhieuNhap], [NgayNhap], [TongTien]) VALUES (15, CAST(N'2024-04-10T00:00:00.000' AS DateTime), 300000)
SET IDENTITY_INSERT [PHIEUNHAPTHUOC] OFF
GO
SET IDENTITY_INSERT [THAMSO] ON 

INSERT [THAMSO] ([MaThamSo], [TenThamSo], [GiaTri]) VALUES (1, N'SoBenhNhanToiDa', 40)
INSERT [THAMSO] ([MaThamSo], [TenThamSo], [GiaTri]) VALUES (2, N'TienKham', 30000)
INSERT [THAMSO] ([MaThamSo], [TenThamSo], [GiaTri]) VALUES (3, N'TiLeTinhDonGiaBan', 1.02)
INSERT [THAMSO] ([MaThamSo], [TenThamSo], [GiaTri]) VALUES (5, N'ApDungTienThuoc', 0)
INSERT [THAMSO] ([MaThamSo], [TenThamSo], [GiaTri]) VALUES (6, N'ApDungTienThuoc', 1)
SET IDENTITY_INSERT [THAMSO] OFF
GO
SET IDENTITY_INSERT [THUOC] ON 

INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (1, 1, N'Panadol', 1, 2, 10000, 20000, 10)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (3, 1, N'Aspirin', 1, 2, 60000, 80000, 20)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (4, 2, N'Efferalgan', 1, 2, 20000, 25000, 4)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (5, 2, N'Ibuprofen', 1, 2, 100000, 110000, 4)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (7, 3, N'Brufen', 1, 2, 115000, 20000, 2)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (8, 6, N'Amoxicillin', 1, 2, 100000, 110000, 5)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (9, 3, N'Diclofenac', 1, 1, 80000, 100000, 5)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (10, 4, N'Omeprazole', 2, 3, 120000, 200000, 10)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (11, 2, N'Tylenol', 1, 2, 5000, 90000, 25)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (12, 3, N'Voltaren', 1, 2, 70000, 80000, 30)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (13, 5, N'Prednisolone', 1, 2, 60000, 70000, 40)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (14, 7, N'Cetirizine', 1, 2, 50000, 60000, 50)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (15, 8, N'Metformin', 1, 2, 90000, 100000, 20)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (16, 11, N'Ranitidine', 2, 3, 120000, 130000, 10)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (17, 12, N'Lisinopril', 1, 2, 130000, 140000, 15)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (18, 13, N'Nifedipine', 1, 2, 140000, 150000, 20)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (19, 15, N'Lamivudine', 1, 2, 160000, 170000, 30)
INSERT [THUOC] ([MaThuoc], [MaLoaiThuoc], [TenThuoc], [MaDVT], [MaCachDung], [DonGiaNhap], [DonGiaBan], [SoLuong]) VALUES (20, 17, N'Calcium Carbonate', 1, 2, 120000, 130000, 25)
SET IDENTITY_INSERT [THUOC] OFF
GO
ALTER TABLE [BAOCAOSUDUNGTHUOC]  WITH CHECK ADD  CONSTRAINT [FK_BAOCAOSUDUNGTHUOC_THUOC] FOREIGN KEY([MaThuoc])
REFERENCES [THUOC] ([MaThuoc])
GO
ALTER TABLE [BAOCAOSUDUNGTHUOC] CHECK CONSTRAINT [FK_BAOCAOSUDUNGTHUOC_THUOC]
GO
ALTER TABLE [CT_BCDT]  WITH CHECK ADD  CONSTRAINT [FK_CT_BCDT_MaBCDT] FOREIGN KEY([MaBCDT])
REFERENCES [BAOCAODOANHTHU] ([MaBCDT])
GO
ALTER TABLE [CT_BCDT] CHECK CONSTRAINT [FK_CT_BCDT_MaBCDT]
GO
ALTER TABLE [CT_BCDT]  WITH CHECK ADD  CONSTRAINT [FK_CT_BCDT_SoHD] FOREIGN KEY([SoHD])
REFERENCES [HOADON] ([SoHD])
GO
ALTER TABLE [CT_BCDT] CHECK CONSTRAINT [FK_CT_BCDT_SoHD]
GO
ALTER TABLE [CT_PKB]  WITH CHECK ADD  CONSTRAINT [FK_CT_PKB_PHIEUKHAMBENH] FOREIGN KEY([MaPKB])
REFERENCES [PHIEUKHAMBENH] ([MaPKB])
GO
ALTER TABLE [CT_PKB] CHECK CONSTRAINT [FK_CT_PKB_PHIEUKHAMBENH]
GO
ALTER TABLE [CT_PKB]  WITH CHECK ADD  CONSTRAINT [FK_CT_PKB_THUOC] FOREIGN KEY([MaThuoc])
REFERENCES [THUOC] ([MaThuoc])
GO
ALTER TABLE [CT_PKB] CHECK CONSTRAINT [FK_CT_PKB_THUOC]
GO
ALTER TABLE [CT_PNT]  WITH CHECK ADD  CONSTRAINT [FK_CT_PNT_MaThuoc] FOREIGN KEY([MaThuoc])
REFERENCES [THUOC] ([MaThuoc])
GO
ALTER TABLE [CT_PNT] CHECK CONSTRAINT [FK_CT_PNT_MaThuoc]
GO
ALTER TABLE [CT_PNT]  WITH CHECK ADD  CONSTRAINT [FK_CT_PNT_SoPhieuNhap] FOREIGN KEY([SoPhieuNhap])
REFERENCES [PHIEUNHAPTHUOC] ([SoPhieuNhap])
GO
ALTER TABLE [CT_PNT] CHECK CONSTRAINT [FK_CT_PNT_SoPhieuNhap]
GO
ALTER TABLE [HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_MaBN] FOREIGN KEY([MaBN])
REFERENCES [BENHNHAN] ([MaBN])
GO
ALTER TABLE [HOADON] CHECK CONSTRAINT [FK_HOADON_MaBN]
GO
ALTER TABLE [HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_MaPKB] FOREIGN KEY([MaPKB])
REFERENCES [PHIEUKHAMBENH] ([MaPKB])
GO
ALTER TABLE [HOADON] CHECK CONSTRAINT [FK_HOADON_MaPKB]
GO
ALTER TABLE [NGUOIDUNG]  WITH CHECK ADD  CONSTRAINT [FK_NGUOIDUNG_BACSI] FOREIGN KEY([MaBS])
REFERENCES [BACSI] ([MaBS])
GO
ALTER TABLE [NGUOIDUNG] CHECK CONSTRAINT [FK_NGUOIDUNG_BACSI]
GO
ALTER TABLE [NGUOIDUNG]  WITH CHECK ADD  CONSTRAINT [FK_NGUOIDUNG_NHOMNGUOIDUNG] FOREIGN KEY([MaNhom])
REFERENCES [NHOMNGUOIDUNG] ([MaNhom])
GO
ALTER TABLE [NGUOIDUNG] CHECK CONSTRAINT [FK_NGUOIDUNG_NHOMNGUOIDUNG]
GO
ALTER TABLE [PHANQUYEN]  WITH CHECK ADD  CONSTRAINT [FK_PHANQUYEN_CHUCNANG] FOREIGN KEY([MaChucNang])
REFERENCES [CHUCNANG] ([MaChucNang])
GO
ALTER TABLE [PHANQUYEN] CHECK CONSTRAINT [FK_PHANQUYEN_CHUCNANG]
GO
ALTER TABLE [PHANQUYEN]  WITH CHECK ADD  CONSTRAINT [FK_PHANQUYEN_NHOMNGUOIDUNG] FOREIGN KEY([MaNhom])
REFERENCES [NHOMNGUOIDUNG] ([MaNhom])
GO
ALTER TABLE [PHANQUYEN] CHECK CONSTRAINT [FK_PHANQUYEN_NHOMNGUOIDUNG]
GO
ALTER TABLE [PHIEUKHAMBENH]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUKHAMBENH_MaBN] FOREIGN KEY([MaBN])
REFERENCES [BENHNHAN] ([MaBN])
GO
ALTER TABLE [PHIEUKHAMBENH] CHECK CONSTRAINT [FK_PHIEUKHAMBENH_MaBN]
GO
ALTER TABLE [PHIEUKHAMBENH]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUKHAMBENH_MaBS] FOREIGN KEY([MaBS])
REFERENCES [BACSI] ([MaBS])
GO
ALTER TABLE [PHIEUKHAMBENH] CHECK CONSTRAINT [FK_PHIEUKHAMBENH_MaBS]
GO
ALTER TABLE [PHIEUKHAMBENH]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUKHAMBENH_MaLoaiBenh] FOREIGN KEY([MaLoaiBenh])
REFERENCES [LOAIBENH] ([MaLoaiBenh])
GO
ALTER TABLE [PHIEUKHAMBENH] CHECK CONSTRAINT [FK_PHIEUKHAMBENH_MaLoaiBenh]
GO
ALTER TABLE [THUOC]  WITH CHECK ADD  CONSTRAINT [FK_THUOC_MaCachDung] FOREIGN KEY([MaCachDung])
REFERENCES [CACHDUNG] ([MaCachDung])
GO
ALTER TABLE [THUOC] CHECK CONSTRAINT [FK_THUOC_MaCachDung]
GO
ALTER TABLE [THUOC]  WITH CHECK ADD  CONSTRAINT [FK_THUOC_MaDVT] FOREIGN KEY([MaDVT])
REFERENCES [DVT] ([MaDVT])
GO
ALTER TABLE [THUOC] CHECK CONSTRAINT [FK_THUOC_MaDVT]
GO
ALTER TABLE [THUOC]  WITH CHECK ADD  CONSTRAINT [FK_THUOC_MaLoaiThuoc] FOREIGN KEY([MaLoaiThuoc])
REFERENCES [LOAITHUOC] ([MaLoaiThuoc])
GO
ALTER TABLE [THUOC] CHECK CONSTRAINT [FK_THUOC_MaLoaiThuoc]
GO
USE [master]
GO
ALTER DATABASE [QUANLYPHONGMACHTU] SET  READ_WRITE 
GO

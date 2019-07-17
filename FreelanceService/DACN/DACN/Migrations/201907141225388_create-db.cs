namespace DACN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BinhLuans",
                c => new
                    {
                        MaBL = c.Int(nullable: false, identity: true),
                        MaSP = c.Int(),
                        NoiDung = c.String(),
                        Sao = c.Int(),
                        NgayBinhLuan = c.DateTime(),
                        MaTaiKhoan = c.Int(),
                    })
                .PrimaryKey(t => t.MaBL)
                .ForeignKey("dbo.SanPhams", t => t.MaSP)
                .ForeignKey("dbo.TaiKhoan", t => t.MaTaiKhoan)
                .Index(t => t.MaSP)
                .Index(t => t.MaTaiKhoan);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        MaSP = c.Int(nullable: false, identity: true),
                        TenSP = c.String(),
                        MotaSP = c.String(),
                        MotaSP2 = c.String(),
                        GiaSP = c.String(),
                        ThoiGianGiao = c.Int(),
                        SoLanSua = c.Int(),
                        HangDoi = c.Int(),
                        DoUuTien = c.Int(),
                        Delete = c.Boolean(),
                        NgayTao = c.DateTime(),
                        NgaySua = c.DateTime(),
                        MaCTDM = c.Int(),
                        MaTaiKhoan = c.Int(),
                    })
                .PrimaryKey(t => t.MaSP)
                .ForeignKey("dbo.CTDMs", t => t.MaCTDM)
                .ForeignKey("dbo.TaiKhoan", t => t.MaTaiKhoan)
                .Index(t => t.MaCTDM)
                .Index(t => t.MaTaiKhoan);
            
            CreateTable(
                "dbo.CTDMs",
                c => new
                    {
                        MaCTDM = c.Int(nullable: false, identity: true),
                        TenCTDM = c.String(),
                        MaDM = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaCTDM)
                .ForeignKey("dbo.DanhMucs", t => t.MaDM, cascadeDelete: true)
                .Index(t => t.MaDM);
            
            CreateTable(
                "dbo.DanhMucs",
                c => new
                    {
                        MaDM = c.Int(nullable: false, identity: true),
                        TenDM = c.String(),
                        DoUuTien = c.Int(),
                    })
                .PrimaryKey(t => t.MaDM);
            
            CreateTable(
                "dbo.TaiKhoan",
                c => new
                    {
                        MaTaiKhoan = c.Int(nullable: false, identity: true),
                        TenTaiKhoan = c.String(nullable: false, maxLength: 64, unicode: false),
                        MatKhau = c.String(nullable: false, maxLength: 256, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        SDT = c.String(nullable: false, maxLength: 12),
                        DiaChi = c.String(nullable: false, maxLength: 256),
                        SoTien = c.String(),
                        SoTKNganHang = c.String(maxLength: 14),
                        TenTkNganHang = c.String(maxLength: 256),
                        MaMatKhau = c.String(),
                        MoTaTaiKhoan = c.String(),
                        HoTen = c.String(),
                    })
                .PrimaryKey(t => t.MaTaiKhoan);
            
            CreateTable(
                "dbo.CTDDHs",
                c => new
                    {
                        MaCTDDH = c.Int(nullable: false, identity: true),
                        MaCTSP = c.Int(),
                        SoLuong = c.Int(),
                        MaDDH = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaCTDDH)
                .ForeignKey("dbo.CTSPs", t => t.MaCTSP)
                .ForeignKey("dbo.DonDatHangs", t => t.MaDDH, cascadeDelete: true)
                .Index(t => t.MaCTSP)
                .Index(t => t.MaDDH);
            
            CreateTable(
                "dbo.CTSPs",
                c => new
                    {
                        MaCTSP = c.Int(nullable: false, identity: true),
                        GiaCTSP = c.String(),
                        MaMetaData = c.Int(),
                        SoLuong = c.Int(),
                        TrangThai = c.Boolean(),
                        MaSP = c.Int(),
                        MaGigMetaData = c.Int(),
                        Delete = c.Boolean(),
                    })
                .PrimaryKey(t => t.MaCTSP)
                .ForeignKey("dbo.GigMetaDatas", t => t.MaGigMetaData)
                .ForeignKey("dbo.MetaDatas", t => t.MaMetaData)
                .ForeignKey("dbo.SanPhams", t => t.MaSP)
                .Index(t => t.MaMetaData)
                .Index(t => t.MaSP)
                .Index(t => t.MaGigMetaData);
            
            CreateTable(
                "dbo.GigMetaDatas",
                c => new
                    {
                        MaGigMetaData = c.Int(nullable: false, identity: true),
                        TenGigMetaData = c.String(),
                        MotaGigMetaData = c.String(),
                        MaCTDM = c.Int(),
                    })
                .PrimaryKey(t => t.MaGigMetaData)
                .ForeignKey("dbo.CTDMs", t => t.MaCTDM)
                .Index(t => t.MaCTDM);
            
            CreateTable(
                "dbo.MetaDatas",
                c => new
                    {
                        MaMetaData = c.Int(nullable: false, identity: true),
                        TenMetaData = c.String(),
                        MaCTDM = c.Int(nullable: false),
                        MaLoaiMetaData = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaMetaData)
                .ForeignKey("dbo.CTDMs", t => t.MaCTDM, cascadeDelete: true)
                .ForeignKey("dbo.LoaiMetaDatas", t => t.MaLoaiMetaData, cascadeDelete: true)
                .Index(t => t.MaCTDM)
                .Index(t => t.MaLoaiMetaData);
            
            CreateTable(
                "dbo.LoaiMetaDatas",
                c => new
                    {
                        MaLoaiMetaData = c.Int(nullable: false, identity: true),
                        TenLoaiMetaData = c.String(),
                    })
                .PrimaryKey(t => t.MaLoaiMetaData);
            
            CreateTable(
                "dbo.DonDatHangs",
                c => new
                    {
                        MaDDH = c.Int(nullable: false, identity: true),
                        NgayDat = c.DateTime(),
                        NgayBatDau = c.DateTime(),
                        NgayGiao = c.DateTime(),
                        TrangThai = c.Boolean(),
                        MoTaDDH = c.String(),
                        SoLuong = c.Int(),
                        Delete = c.Boolean(),
                        GiaDDH = c.String(),
                        MaTaiKhoan = c.Int(),
                        MaSP = c.Int(),
                        NgayNhanTien = c.DateTime(),
                        SoTienNhan = c.String(),
                    })
                .PrimaryKey(t => t.MaDDH)
                .ForeignKey("dbo.SanPhams", t => t.MaSP)
                .ForeignKey("dbo.TaiKhoan", t => t.MaTaiKhoan)
                .Index(t => t.MaTaiKhoan)
                .Index(t => t.MaSP);
            
            CreateTable(
                "dbo.DatLaiMKModels",
                c => new
                    {
                        MatKhauMoi = c.String(nullable: false, maxLength: 128),
                        XacNhanMK = c.String(),
                        DatLaiMa = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MatKhauMoi);
            
            CreateTable(
                "dbo.DonHangHoanThanhs",
                c => new
                    {
                        MaDHHT = c.Int(nullable: false, identity: true),
                        NgayHoanThanh = c.DateTime(),
                        TrangThai = c.Boolean(),
                        NoiDungDHHT = c.String(),
                        MaTaiKhoan = c.Int(),
                        MaDDH = c.Int(),
                        DuongDanChinh = c.String(),
                        DuongDanPhu = c.String(),
                    })
                .PrimaryKey(t => t.MaDHHT)
                .ForeignKey("dbo.DonDatHangs", t => t.MaDDH)
                .ForeignKey("dbo.TaiKhoan", t => t.MaTaiKhoan)
                .Index(t => t.MaTaiKhoan)
                .Index(t => t.MaDDH);
            
            CreateTable(
                "dbo.HinhAnhs",
                c => new
                    {
                        MaHinhAnh = c.Int(nullable: false, identity: true),
                        TenHinhAnh = c.String(),
                        MaSP = c.Int(),
                        MaDHHT = c.Int(),
                        MaTienDo = c.Int(),
                        MaTaiKhoan = c.Int(),
                        Delete = c.Boolean(),
                    })
                .PrimaryKey(t => t.MaHinhAnh)
                .ForeignKey("dbo.DonHangHoanThanhs", t => t.MaDHHT)
                .ForeignKey("dbo.SanPhams", t => t.MaSP)
                .ForeignKey("dbo.TaiKhoan", t => t.MaTaiKhoan)
                .ForeignKey("dbo.TienDoCongViecs", t => t.MaTienDo)
                .Index(t => t.MaSP)
                .Index(t => t.MaDHHT)
                .Index(t => t.MaTienDo)
                .Index(t => t.MaTaiKhoan);
            
            CreateTable(
                "dbo.TienDoCongViecs",
                c => new
                    {
                        MaTienDo = c.Int(nullable: false, identity: true),
                        NgayGui = c.DateTime(),
                        NoiDung = c.String(),
                        MucTienDo = c.Int(),
                        MaDDH = c.Int(),
                    })
                .PrimaryKey(t => t.MaTienDo)
                .ForeignKey("dbo.DonDatHangs", t => t.MaDDH)
                .Index(t => t.MaDDH);
            
            CreateTable(
                "dbo.PhanQuyen",
                c => new
                    {
                        MaQuyen = c.Int(nullable: false),
                        MaTaiKhoan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MaQuyen, t.MaTaiKhoan })
                .ForeignKey("dbo.Quyen", t => t.MaQuyen, cascadeDelete: true)
                .ForeignKey("dbo.TaiKhoan", t => t.MaTaiKhoan, cascadeDelete: true)
                .Index(t => t.MaQuyen)
                .Index(t => t.MaTaiKhoan);
            
            CreateTable(
                "dbo.Quyen",
                c => new
                    {
                        MaQuyen = c.Int(nullable: false, identity: true),
                        TenQuyen = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.MaQuyen);
            
            CreateTable(
                "dbo.TapTins",
                c => new
                    {
                        MaTapTin = c.Int(nullable: false, identity: true),
                        DuongDan = c.String(),
                        TenTapTin = c.String(),
                        MaTinNhan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaTapTin)
                .ForeignKey("dbo.TinNhans", t => t.MaTinNhan, cascadeDelete: true)
                .Index(t => t.MaTinNhan);
            
            CreateTable(
                "dbo.TinNhans",
                c => new
                    {
                        MaTinNhan = c.Int(nullable: false, identity: true),
                        ThoiGian = c.DateTime(nullable: false),
                        NoiDung = c.String(),
                        MaDDH = c.Int(),
                        MaTaiKhoan = c.Int(),
                        SellerSeen = c.Boolean(),
                        BuyerSeen = c.Boolean(),
                    })
                .PrimaryKey(t => t.MaTinNhan)
                .ForeignKey("dbo.DonDatHangs", t => t.MaDDH)
                .ForeignKey("dbo.TaiKhoan", t => t.MaTaiKhoan)
                .Index(t => t.MaDDH)
                .Index(t => t.MaTaiKhoan);
            
            CreateTable(
                "dbo.ThanhToans",
                c => new
                    {
                        MaThanhToan = c.Int(nullable: false, identity: true),
                        MaDDH = c.Int(nullable: false),
                        NoiDung = c.String(),
                        ThoiGian = c.Time(nullable: false, precision: 7),
                        SoTien = c.Long(nullable: false),
                        MaTKNhanTien = c.Int(),
                        MaTKTraTien = c.Int(),
                    })
                .PrimaryKey(t => t.MaThanhToan)
                .ForeignKey("dbo.DonDatHangs", t => t.MaDDH, cascadeDelete: true)
                .Index(t => t.MaDDH);
            
            CreateTable(
                "dbo.ThemMoTaDDHs",
                c => new
                    {
                        MaMoTaDDH = c.Int(nullable: false, identity: true),
                        MoTa = c.String(),
                        MaDDH = c.Int(),
                        ThoiGian = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MaMoTaDDH)
                .ForeignKey("dbo.DonDatHangs", t => t.MaDDH)
                .Index(t => t.MaDDH);
            
            CreateTable(
                "dbo.ThongBaos",
                c => new
                    {
                        MaThongBao = c.Int(nullable: false, identity: true),
                        MaDDH = c.Int(nullable: false),
                        NoiDung = c.String(),
                        ThoiGian = c.DateTime(nullable: false),
                        MaTaiKhoan = c.Int(),
                        SellerSeen = c.Boolean(),
                        BuyerSeen = c.Boolean(),
                    })
                .PrimaryKey(t => t.MaThongBao)
                .ForeignKey("dbo.DonDatHangs", t => t.MaDDH, cascadeDelete: true)
                .ForeignKey("dbo.TaiKhoan", t => t.MaTaiKhoan)
                .Index(t => t.MaDDH)
                .Index(t => t.MaTaiKhoan);
            
            CreateTable(
                "dbo.TranhChaps",
                c => new
                    {
                        MaTranhChap = c.Int(nullable: false, identity: true),
                        MaDDH = c.Int(nullable: false),
                        LienHe = c.String(),
                        NoiDung = c.String(),
                        ThoiGian = c.DateTime(nullable: false),
                        TrangThai = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaTranhChap)
                .ForeignKey("dbo.DonDatHangs", t => t.MaDDH, cascadeDelete: true)
                .Index(t => t.MaDDH);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TranhChaps", "MaDDH", "dbo.DonDatHangs");
            DropForeignKey("dbo.ThongBaos", "MaTaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.ThongBaos", "MaDDH", "dbo.DonDatHangs");
            DropForeignKey("dbo.ThemMoTaDDHs", "MaDDH", "dbo.DonDatHangs");
            DropForeignKey("dbo.ThanhToans", "MaDDH", "dbo.DonDatHangs");
            DropForeignKey("dbo.TapTins", "MaTinNhan", "dbo.TinNhans");
            DropForeignKey("dbo.TinNhans", "MaTaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.TinNhans", "MaDDH", "dbo.DonDatHangs");
            DropForeignKey("dbo.PhanQuyen", "MaTaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.PhanQuyen", "MaQuyen", "dbo.Quyen");
            DropForeignKey("dbo.HinhAnhs", "MaTienDo", "dbo.TienDoCongViecs");
            DropForeignKey("dbo.TienDoCongViecs", "MaDDH", "dbo.DonDatHangs");
            DropForeignKey("dbo.HinhAnhs", "MaTaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.HinhAnhs", "MaSP", "dbo.SanPhams");
            DropForeignKey("dbo.HinhAnhs", "MaDHHT", "dbo.DonHangHoanThanhs");
            DropForeignKey("dbo.DonHangHoanThanhs", "MaTaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.DonHangHoanThanhs", "MaDDH", "dbo.DonDatHangs");
            DropForeignKey("dbo.CTDDHs", "MaDDH", "dbo.DonDatHangs");
            DropForeignKey("dbo.DonDatHangs", "MaTaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.DonDatHangs", "MaSP", "dbo.SanPhams");
            DropForeignKey("dbo.CTDDHs", "MaCTSP", "dbo.CTSPs");
            DropForeignKey("dbo.CTSPs", "MaSP", "dbo.SanPhams");
            DropForeignKey("dbo.CTSPs", "MaMetaData", "dbo.MetaDatas");
            DropForeignKey("dbo.MetaDatas", "MaLoaiMetaData", "dbo.LoaiMetaDatas");
            DropForeignKey("dbo.MetaDatas", "MaCTDM", "dbo.CTDMs");
            DropForeignKey("dbo.CTSPs", "MaGigMetaData", "dbo.GigMetaDatas");
            DropForeignKey("dbo.GigMetaDatas", "MaCTDM", "dbo.CTDMs");
            DropForeignKey("dbo.BinhLuans", "MaTaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.BinhLuans", "MaSP", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "MaTaiKhoan", "dbo.TaiKhoan");
            DropForeignKey("dbo.SanPhams", "MaCTDM", "dbo.CTDMs");
            DropForeignKey("dbo.CTDMs", "MaDM", "dbo.DanhMucs");
            DropIndex("dbo.TranhChaps", new[] { "MaDDH" });
            DropIndex("dbo.ThongBaos", new[] { "MaTaiKhoan" });
            DropIndex("dbo.ThongBaos", new[] { "MaDDH" });
            DropIndex("dbo.ThemMoTaDDHs", new[] { "MaDDH" });
            DropIndex("dbo.ThanhToans", new[] { "MaDDH" });
            DropIndex("dbo.TinNhans", new[] { "MaTaiKhoan" });
            DropIndex("dbo.TinNhans", new[] { "MaDDH" });
            DropIndex("dbo.TapTins", new[] { "MaTinNhan" });
            DropIndex("dbo.PhanQuyen", new[] { "MaTaiKhoan" });
            DropIndex("dbo.PhanQuyen", new[] { "MaQuyen" });
            DropIndex("dbo.TienDoCongViecs", new[] { "MaDDH" });
            DropIndex("dbo.HinhAnhs", new[] { "MaTaiKhoan" });
            DropIndex("dbo.HinhAnhs", new[] { "MaTienDo" });
            DropIndex("dbo.HinhAnhs", new[] { "MaDHHT" });
            DropIndex("dbo.HinhAnhs", new[] { "MaSP" });
            DropIndex("dbo.DonHangHoanThanhs", new[] { "MaDDH" });
            DropIndex("dbo.DonHangHoanThanhs", new[] { "MaTaiKhoan" });
            DropIndex("dbo.DonDatHangs", new[] { "MaSP" });
            DropIndex("dbo.DonDatHangs", new[] { "MaTaiKhoan" });
            DropIndex("dbo.MetaDatas", new[] { "MaLoaiMetaData" });
            DropIndex("dbo.MetaDatas", new[] { "MaCTDM" });
            DropIndex("dbo.GigMetaDatas", new[] { "MaCTDM" });
            DropIndex("dbo.CTSPs", new[] { "MaGigMetaData" });
            DropIndex("dbo.CTSPs", new[] { "MaSP" });
            DropIndex("dbo.CTSPs", new[] { "MaMetaData" });
            DropIndex("dbo.CTDDHs", new[] { "MaDDH" });
            DropIndex("dbo.CTDDHs", new[] { "MaCTSP" });
            DropIndex("dbo.CTDMs", new[] { "MaDM" });
            DropIndex("dbo.SanPhams", new[] { "MaTaiKhoan" });
            DropIndex("dbo.SanPhams", new[] { "MaCTDM" });
            DropIndex("dbo.BinhLuans", new[] { "MaTaiKhoan" });
            DropIndex("dbo.BinhLuans", new[] { "MaSP" });
            DropTable("dbo.TranhChaps");
            DropTable("dbo.ThongBaos");
            DropTable("dbo.ThemMoTaDDHs");
            DropTable("dbo.ThanhToans");
            DropTable("dbo.TinNhans");
            DropTable("dbo.TapTins");
            DropTable("dbo.Quyen");
            DropTable("dbo.PhanQuyen");
            DropTable("dbo.TienDoCongViecs");
            DropTable("dbo.HinhAnhs");
            DropTable("dbo.DonHangHoanThanhs");
            DropTable("dbo.DatLaiMKModels");
            DropTable("dbo.DonDatHangs");
            DropTable("dbo.LoaiMetaDatas");
            DropTable("dbo.MetaDatas");
            DropTable("dbo.GigMetaDatas");
            DropTable("dbo.CTSPs");
            DropTable("dbo.CTDDHs");
            DropTable("dbo.TaiKhoan");
            DropTable("dbo.DanhMucs");
            DropTable("dbo.CTDMs");
            DropTable("dbo.SanPhams");
            DropTable("dbo.BinhLuans");
        }
    }
}

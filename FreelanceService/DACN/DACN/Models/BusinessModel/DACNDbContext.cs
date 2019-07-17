using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACN.Models.DataModel;
using System.Data.Entity;

namespace DACN.Models.BusinessModel
{
    public class DACNDbContext: DbContext
    {
        public DACNDbContext() : base("name=DACNConnection")
        {

        }
        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<PhanQuyen> PhanQuyens { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<CTDM> CTDMs { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<DonDatHang> DonDatHangs { get; set; }
        public DbSet<HinhAnh> HinhAnhs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TapTin> TapTins { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }
        public DbSet<TinNhan> TinNhans { get; set; }
        public DbSet<TranhChap> TranhChaps { get; set; }
        public DbSet<CTSP> CTSP { get; set; }
        public DbSet<MetaData> MetaDatas { get; set; }
        public DbSet<LoaiMetaData> LoaiMetaData { get; set; }
        public DbSet<CTDDH> CTDDHs { get; set; }
        public DbSet<DatLaiMKModel> DatLaiMKModels { get; set; }
        public DbSet<GigMetaData> GigMetaDatas { get; set; }
        public DbSet<DonHangHoanThanh> DonHangHoanThanhs { get; set; }
        public DbSet<TienDoCongViec> TienDoCongViecs { get; set; }
        public DbSet<ThemMoTaDDH> ThemMoTaDDHs { get; set; }
    }
}
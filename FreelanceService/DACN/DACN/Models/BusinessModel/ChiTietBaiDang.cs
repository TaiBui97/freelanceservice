using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class ChiTietBaiDang
    {
        public List<CTSP> listCTSP { get; set; }
        public List<BinhLuan> listDanhGia { get; set; }
        public List<BinhLuan> listDanhGiaRandom { get; set; }
        public List<TaiKhoan> listTaiKhoanDanhGia { get; set; }
        public List<HinhAnh> listHinhTK { get; set; }
        public SanPham SanPham { get; set; }
        public HinhAnh TenHinhDaiDien { get; set; }
        public TaiKhoan taikhoan { get; set; }
        public List<string> TenMetaData { get; set; }
        public List<string> HinhSanPham { get; set; }
        public string HinhSPDauTien { get; set; }
        public List<SanPham> randomBaiDang { get; set; }
        public List<HinhAnh> randomHinhBD { get; set; }
        public List<TaiKhoan> listTKRandom { get; set; }
        public List<HinhAnh> listHinhTKRandom { get; set; }
    }
}
using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class DanhSachBaiDang
    {
        public List<SanPham> SanPham { get; set; }
        public List<BinhLuan> listDanhGia { get; set; }
        public List<HinhAnh> TenHinhDaiDien { get; set; }
        public List<TaiKhoan> TaiKhoan { get; set; }
        public List<CTDM> listCTDM { get; set; }
        public List<GigMetaData> listGig { get; set; }
        public List<MetaData> listMeta { get; set; }
        public int idFilter { get; set; }
    }
}
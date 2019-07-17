using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class OrderChiTiet
    {
        public SanPham Baidang { get; set; }
        public HinhAnh hinh { get; set; }
        public List<DonHangHoanThanh> listDHHT{ get; set; }
        public List<GigMetaData> listgigkh { get; set; }
        public DonDatHang donDatHang { get; set; }
        public List<GigMetaData> listgigbd { get; set; }
        public List<TinNhan> listMess { get; set; }
        public List<CTSP> listGia { get; set; }
        public List<TaiKhoan> listTaikhoan { get; set; }
        public List<TienDoCongViec> listTienDo { get; set; }
        public TaiKhoan tkMinhChung { get; set; }
        public TaiKhoan tkBuyer { get; set; }
        public List<HinhAnh> listHinhMinhChung { get; set; }
        public List<HinhAnh> listHinhTienDo { get; set; }
        public List<ThemMoTaDDH> listMotaDDH { get; set; }
        public static List<TaiKhoan> listOrderChitiet { get; set; }
    }
}
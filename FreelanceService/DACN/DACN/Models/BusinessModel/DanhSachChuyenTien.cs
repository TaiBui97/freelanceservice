using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class DanhSachChuyenTien
    {
        public List<DonDatHang> listDonDatHang { get; set; }
        public List<TaiKhoan> listTaiKhoan { get; set; }
        public List<SanPham> listSanPham { get; set; }
    }
}
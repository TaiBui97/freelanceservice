using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class DanhSachDHHT
    {
        public List<DonDatHang> donDatHangs { get; set; }
        public List<DonHangHoanThanh> donHangHoanThanhs { get; set; }
        public List<TaiKhoan> listTaiKhoan { get; set; }
        public List<SanPham> listSanPham { get; set; }
    }
}
using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class DanhSachTranhChap
    {
        public List<DonDatHang> donDatHangs { get; set; }
        public List<TranhChap> tranhChaps { get; set; }
        public List<TaiKhoan> Buyer { get; set; }
        public List<TaiKhoan> Seller { get; set; }
        public List<SanPham> listSanPham { get; set; }
    }
}
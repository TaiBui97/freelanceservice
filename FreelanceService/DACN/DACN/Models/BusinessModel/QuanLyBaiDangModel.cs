using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACN.Models.DataModel;
namespace DACN.Models.BusinessModel
{
    public class QuanLyBaiDangModel
    {
        public List<HienThiBaiDang> listCTSP { get; set; }
        public HinhAnh TenHinhDaiDien { get; set; }
        public TaiKhoan taikhoan { get; set; }
    }
}
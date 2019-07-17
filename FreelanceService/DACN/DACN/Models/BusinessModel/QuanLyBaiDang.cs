using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACN.Models.DataModel;
namespace DACN.Models.BusinessModel
{
    public class QuanLyBaiDang
    {
        public List<CTSP> listCTSP { get; set; }
        public TaiKhoan userAccount { get; set; }
        public SanPham listSP { get; set; }
    }
}
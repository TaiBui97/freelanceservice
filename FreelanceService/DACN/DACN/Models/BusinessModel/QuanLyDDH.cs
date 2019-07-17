using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class QuanLyDDH
    {
        public List<DonDatHang> myListDDH { get; set; }
        public List<DonDatHang> customerListDDH { get; set; }
        public List<TaiKhoan> customerListTK { get; set; }
        public List<TaiKhoan> myBuyerListTK { get; set; }
        public TaiKhoan myTk { get; set; }
        public List<SanPham> myListSP { get; set; }
        public List<SanPham> myBuyerListSP { get; set; }
        public List<GigMetaData> listGigOffer { get; set; }
        public List<CTSP> listctsp { get; set; }
    }
}
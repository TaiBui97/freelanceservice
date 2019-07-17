using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class EditService
    {
        public SanPham Sanpham { get; set; }
        public List<MetaData> Metadata { get; set; }
        public LoaiMetaData LoaiMetadata { get; set; }
        public List<GigMetaData> GigMetadata { get; set; }
        public List<DanhMuc> danhmuc { get; set; }
        public List<CTDM> listctdm { get; set; }
        public List<HinhAnh> hinh { get; set; }
        public static List<EditService> ds = new List<EditService>();
    }
}




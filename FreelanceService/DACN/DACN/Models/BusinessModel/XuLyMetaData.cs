using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class XuLyMetaData
    {
        public int Ma { get; set; }
        public string GigMetaData2 { get; set; }
        public string Tien { get; set; }
        public int SL { get; set; }
        public bool TrangThai { get; set; }
        public static List<XuLyMetaData> dsXuly = new List<XuLyMetaData>();
    }
}
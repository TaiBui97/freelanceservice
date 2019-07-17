using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class HienThiThongBao
    {
        public List<TinNhan> listTinNhan { get; set; }
        public List<ThongBao> listThongBao { get; set; }
    }
}
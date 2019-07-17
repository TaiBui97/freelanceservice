using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class Navigation
    {
        public List<DanhMuc> listDM { get; set; }
        public List<CTDM> listCTDM { get; set; }
    }
}
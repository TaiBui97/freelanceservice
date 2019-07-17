﻿using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class EditOrder
    {
        public List<GigMetaData> listGigOffer { get; set; }
        public SanPham Baidang { get; set; }
        public HinhAnh hinh { get; set; }
        public CTDM ctdm { get; set; }
        public List<MetaData> listmeta { get; set; }
        public List<CTSP> listctsp { get; set; }
        public DonDatHang ddh { get; set; }
        public static string giaddh { get; set; }
    }
}
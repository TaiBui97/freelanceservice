﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class ThongBao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaThongBao { get; set; }
        [ForeignKey("DonDatHang")]
        public int MaDDH { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        [ForeignKey("TaiKhoan")]
        public int? MaTaiKhoan { get; set; }
        public bool? SellerSeen { get; set; }
        public bool? BuyerSeen { get; set; }
        public virtual DonDatHang DonDatHang { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
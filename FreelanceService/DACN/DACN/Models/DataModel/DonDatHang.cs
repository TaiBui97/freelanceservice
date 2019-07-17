using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class DonDatHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDDH { get; set; }
        public DateTime? NgayDat { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayGiao { get; set; }
        public bool? TrangThai { get; set; }
        public string MoTaDDH { get; set; }
        public int? SoLuong { get; set; }
        public bool? Delete { get; set; }
        public string GiaDDH { get; set; }
        [ForeignKey("TaiKhoan")]
        public int? MaTaiKhoan { get; set; }
        [ForeignKey("SanPham")]
        public int? MaSP { get; set; }
        public DateTime? NgayNhanTien { get; set; }
        public string SoTienNhan { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
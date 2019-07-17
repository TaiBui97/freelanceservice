using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DACN.Models.DataModel
{
    public class DonHangHoanThanh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDHHT { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public bool? TrangThai { get; set; }
        public string NoiDungDHHT { get; set; }
        [ForeignKey("TaiKhoan")]
        public int? MaTaiKhoan { get; set; }
        [ForeignKey("DonDatHang")]
        public int? MaDDH { get; set; }
        public string DuongDanChinh { get; set; }
        public string DuongDanPhu { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual DonDatHang DonDatHang { get; set; }
    }
}
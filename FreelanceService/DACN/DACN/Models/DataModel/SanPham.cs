using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string MotaSP { get; set; }
        public string MotaSP2 { get; set; }
        public string GiaSP { get; set; }
        public int? ThoiGianGiao { get; set; }
        public int? SoLanSua { get; set; }
        public int? HangDoi { get; set; }
        public int? DoUuTien { get; set; }
        public bool? Delete { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        [ForeignKey("CTDM")]
        public int? MaCTDM { get; set; }
        [ForeignKey("TaiKhoan")]
        public int? MaTaiKhoan { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual CTDM CTDM { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class HinhAnh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHinhAnh { get; set; }
        public string TenHinhAnh { get; set; }
        [ForeignKey("SanPham")]
        public int? MaSP { get; set; }
        [ForeignKey("DonHangHoanThanh")]
        public int? MaDHHT { get; set; }
        [ForeignKey("TienDoCongViec")]
        public int? MaTienDo { get; set; }
        [ForeignKey("TaiKhoan")]
        public int? MaTaiKhoan { get; set; }
        public bool? Delete { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual DonHangHoanThanh DonHangHoanThanh { get; set; }
        public virtual TienDoCongViec TienDoCongViec { get; set; }
    }
}
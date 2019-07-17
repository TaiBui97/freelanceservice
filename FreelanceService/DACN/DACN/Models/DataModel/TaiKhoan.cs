using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã tài khoản")]
        public int MaTaiKhoan { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên người dùng")]
        [StringLength(64, ErrorMessage = "Tên tài khoản phải trong khoản ký tự 3- 64 ký tự", MinimumLength = 3)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Tên tài khoản")]
        public string TenTaiKhoan { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [MaxLength(256)]
        [Column(TypeName = "varchar")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không đúng địng dạng")]
        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Hãy nhập số điện thoại")]
        [MaxLength(12)]
        public string SDT { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Hãy nhập địa chỉ")]
        [MaxLength(256)]
        public string DiaChi { get; set; }

        [Display(Name = "Số tiền")]
        public string SoTien { get; set; }

        [Display(Name = "Số tài khoản ngân hàng")]
        [MaxLength(14)]
        public string SoTKNganHang { get; set; }

        [Display(Name = "Tên tài khoản ngân hàng")]
        [MaxLength(256)]
        public string TenTkNganHang { get; set; }

        public string MaMatKhau { get; set; }
        public string MoTaTaiKhoan { get; set; }
        public string HoTen { get; set; }
    }
}

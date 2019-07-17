using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace DACN.Models.DataModel
{
    public class DatLaiMKModel
    {
        [Key]
        [Required(ErrorMessage = "Nhập mật khẩu mới", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; }

        [DataType(DataType.Password)]
        [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string XacNhanMK { get; set; }

        [Required]
        public string DatLaiMa { get; set; }
    }
}

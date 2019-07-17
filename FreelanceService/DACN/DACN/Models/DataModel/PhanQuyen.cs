using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    [Table("PhanQuyen")]
    public class PhanQuyen
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Quyen")]
        [Required]
        public int MaQuyen { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("TaiKhoan")]
        [Required]
        public int MaTaiKhoan { get; set; }

        public virtual Quyen Quyen { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
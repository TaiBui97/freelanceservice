using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class TranhChap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTranhChap { get; set; }
        [ForeignKey("DonDatHang")]
        public int MaDDH { get; set; }
        public string LienHe { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public bool TrangThai { get; set; }
        public virtual DonDatHang DonDatHang { get; set; }
    }
}
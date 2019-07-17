using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class ThanhToan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaThanhToan { get; set; }
        [ForeignKey("DonDatHang")]
        public int MaDDH { get; set; }
        public string NoiDung { get; set; }
        public TimeSpan ThoiGian { get; set; }
        public Int64 SoTien { get; set; }
        public int? MaTKNhanTien { get; set; }
        public int? MaTKTraTien { get; set; }
        public virtual DonDatHang DonDatHang { get; set; }
    }
}
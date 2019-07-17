using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DACN.Models.DataModel
{
    public class CTDDH
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTDDH { get; set; }
        [ForeignKey("CTSP")]
        public int? MaCTSP { get; set; }
        public int? SoLuong { get; set; }
        [ForeignKey("DonDatHang")]
        public int MaDDH { get; set; }
        public virtual CTSP CTSP { get; set; }
        public virtual DonDatHang DonDatHang { get; set; }
    }
}
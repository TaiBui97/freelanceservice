using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DACN.Models.DataModel
{
    public class CTDHHT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTDHHT { get; set; }
        public string DuongDanChinh { get; set; }
        public string DuongDanPhu { get; set; }

        public int? MaDHHT { get; set; }
        
        [ForeignKey("MaDHHT")]
        public virtual DonHangHoanThanh DHHT { get; set; }

    }
}
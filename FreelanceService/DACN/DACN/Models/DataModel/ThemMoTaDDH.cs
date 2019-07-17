using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DACN.Models.DataModel
{
    public class ThemMoTaDDH
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaMoTaDDH { get; set; }
        public string MoTa { get; set; }
        [ForeignKey("DonDatHang")]
        public int? MaDDH { get; set; }
        public DateTime ThoiGian { get; set; }
        public virtual DonDatHang DonDatHang { get; set; }
    }
}
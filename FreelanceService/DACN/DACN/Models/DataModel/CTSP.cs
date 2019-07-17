using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class CTSP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTSP { get; set; }
        public string GiaCTSP { get; set; }
        [ForeignKey("MetaData")]
        public int? MaMetaData { get; set; }
        public int? SoLuong { get; set; }
        public bool? TrangThai { get; set; }
        [ForeignKey("SanPham")]
        public int? MaSP { get; set; }
        [ForeignKey("GigMetaData")]
        public int? MaGigMetaData { get; set; }
        public bool? Delete { get; set; }
        public virtual GigMetaData GigMetaData { get; set; }
        public virtual MetaData MetaData { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class MetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaMetaData { get; set; }
        public string TenMetaData { get; set; }
        [ForeignKey("CTDM")]
        public int MaCTDM { get; set; }
        public int MaLoaiMetaData { get; set; }
        public virtual CTDM CTDM { get; set; }
        public virtual LoaiMetaData LoaiMetaData { get; set; }
    }
}
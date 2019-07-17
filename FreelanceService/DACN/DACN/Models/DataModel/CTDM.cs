using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class CTDM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCTDM { get; set; }
        public string TenCTDM { get; set; }
        [ForeignKey("DanhMuc")]
        public int MaDM { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }
    }
}
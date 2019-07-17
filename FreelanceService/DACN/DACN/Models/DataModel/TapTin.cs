using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class TapTin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTapTin { get; set; }
        public string DuongDan { get; set; }
        public string TenTapTin { get; set; }
        [ForeignKey("TinNhan")]
        public int MaTinNhan { get; set; }
        public virtual TinNhan TinNhan { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class GigMetaData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaGigMetaData { get; set; }
        public string TenGigMetaData { get; set; }
        public string MotaGigMetaData { get; set; }
        [ForeignKey("CTDM")]
        public int? MaCTDM { get; set; }
        public virtual CTDM CTDM { get; set; }
    }
}
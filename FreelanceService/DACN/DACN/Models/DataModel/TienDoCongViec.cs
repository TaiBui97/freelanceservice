using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACN.Models.DataModel
{
    public class TienDoCongViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTienDo { get; set; }
        public DateTime? NgayGui { get; set; }
        public string NoiDung { get; set; }
        public int? MucTienDo { get; set; }
        [ForeignKey("DonDatHang")]
        public int? MaDDH { get; set; }
        public virtual DonDatHang DonDatHang { get; set; }
    }
}
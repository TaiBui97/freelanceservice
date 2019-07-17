using DACN.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DACN.Models.BusinessModel
{
    public class Messenger
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TinNhan tinNhan { get; set; }
    }
}
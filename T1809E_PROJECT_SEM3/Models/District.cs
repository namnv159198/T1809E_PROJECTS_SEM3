using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T1809E_PROJECT_SEM3.Models
{
    public class District
    {
        [Key]
        public int id { get; set; }
        public string _name { get; set; }
        public string _prefix { get; set; }

        public int province_id { get; set; }

        [ForeignKey("province_id")]
        public virtual Provinces Province { get; set; }
    }
}
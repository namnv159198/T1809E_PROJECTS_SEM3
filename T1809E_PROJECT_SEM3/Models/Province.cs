using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace T1809E_PROJECT_SEM3.Models
{
    public class Province
    {
        [Key]
        public int id { get; set; }
        public string _name { get; set; }
        public string _code { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T1809E_PROJECT_SEM3.Models
{
    public class calPrice
    {
        public string ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public Double Distance { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public Double Weight { get; set; }


        public Double PriceShip { get; set; }

        public int TypeItemId { get; set; }
        [ForeignKey("TypeItemId")]
        public virtual TypeItem TypeItem { get; set; }
    }

   
}
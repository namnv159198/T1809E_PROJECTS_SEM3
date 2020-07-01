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
        public double Distance { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Weight { get; set; }


        public double PriceShip { get; set; }

        public string Display { get; set; }

        public int TypeItemId { get; set; }
        [ForeignKey("TypeItemId")]
        public virtual TypeItem TypeItem { get; set; }
    }

   
}
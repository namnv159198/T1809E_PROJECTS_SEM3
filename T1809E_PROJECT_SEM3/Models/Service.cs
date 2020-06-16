using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace T1809E_PROJECT_SEM3.Models
{
    public class Service
    {
        [Key]
        public string ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [Display(Name = "Price Per Km")]
        [Required]
        [Range(typeof(double), "0", "79228162514264337593543950335", ErrorMessage = "The Field Price Can Not < 0")]
        public double PricePerKm { get; set; }
        [Display(Name = "Price Per Kg")]
        [Required]
        [Range(typeof(double), "0", "79228162514264337593543950335", ErrorMessage = "The Field Price Can Not < 0")]
        public double PricePerKg { get; set; }
    }
}
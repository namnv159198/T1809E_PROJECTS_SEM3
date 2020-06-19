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
        [Range (double.Epsilon, double.MaxValue , ErrorMessage = "The 'Price per Km' field must be larger than 0")]
        public double PricePerKm { get; set; }
        [Display(Name = "Price Per Kg")]
        [Required]
        [Range (double.Epsilon, double.MaxValue, ErrorMessage = "The 'Price per Kg' field must be larger than 0")]
        public double PricePerKg { get; set; }
        [Display(Name = "Status")]
        public StatusEnumService Status { get; set; }
        public enum StatusEnumService
        {
            online = 1,
            offline = 0
        }
    }
}
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
        [Display(Name = "Price Step")]
        [Required]
        [Range (double.Epsilon, double.MaxValue , ErrorMessage = "The {0} field must be larger than 0")]
        
        public double PriceStep { get; set; }
        [Display(Name = "Coefficient Weight")]
        [Required]
        [Range (double.Epsilon, double.MaxValue, ErrorMessage = "The {0} field must be larger than 0")]
        public double PriceWeight { get; set; }
      
        [Required]
        [Display(Name ="Distance Step")]
        [Range(0, int.MaxValue,ErrorMessage = "The {0} field must be larger than 0")]
        public int DistanceStep { get; set; }
      
        public string Description { get; set; }
      
        [Display(Name = "Status")]
        public StatusEnumService Status { get; set; }
        public enum StatusEnumService
        {
            Online = 1,
            Offline = 0,
            Deleted = -1
        }
    }
}
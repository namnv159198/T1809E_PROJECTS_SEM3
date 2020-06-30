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
        public EnumServiceType TypeDelivery { get; set; }

        public enum EnumServiceType
        {
            [Display(Name = "Fast Delivery")]
            Fast_Delivery = 1,
            [Display(Name = "Savings Delivery")]
            Savings_Delivery = 2,
            VPP = 3,
            [Display(Name = "Delivery of the day")]
            Delivery_of_the_day = 3
        }

       

        [Display(Name = "From")]
        [Required]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "The {0} field must be larger than 0")]
        public double From { get; set; }

        [Display(Name = "To")]
        [Required]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "The {0} field must be larger than 0")]
        public double To { get; set; }

        [Display(Name = "Price")]
        [Required]
        [Range (double.Epsilon, double.MaxValue , ErrorMessage = "The {0} field must be larger than 0")]
        public double PriceStep { get; set; }

        [Required]
        public EnumType TypeCaculalor { get; set; }
        public enum EnumType
        {
            Distance = 1,
            Weight = 2
        }

        [Required]
        [DataType(DataType.MultilineText)]
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
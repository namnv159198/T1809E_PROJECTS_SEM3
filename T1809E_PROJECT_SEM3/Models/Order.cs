using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T1809E_PROJECT_SEM3.Models
{
    public class Order
    {
        [Key]
        public String ID { get; set; }
        [StringLength(50)]
        public String SenderName { get; set; }
        [Required]
        [StringLength(50)]
        public String SenderAddress { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public String SenderPhone { get; set; }
        [Required]
        [StringLength(50)]
        public String ReceiverName { get; set; }
        [Required]
        public String ReceiverAddress { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public String ReceiverPhone { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public Double Distance { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public Double Weight { get; set; }
        public DateTime? CreateAt { get; set; }

        public Double PriceShip { get; set; }
        public EnumStatusOrder Status { get; set; }
        public enum EnumStatusOrder
        {
            
            Packaging = 0,
            ShippingToOffice = 1,
            ShippingToHouse = 2,
            Shipped = 3,
            Finished = 4,
            Cancelled = 5,
            Deleted = 6,
            New = -1
        }
        /* [Display(Name = "Created By")]
         public String CreatedByName { get; set; }
         [ForeignKey("CreatedByName")]
         public virtual ApplicationUser CreatedBy { get; set; }

         [Display(Name = "Updated By")]
         public String UpdatedByName { get; set; }
         [ForeignKey ("UpdateByName")]
         public virtual ApplicationUser UpdateBy { get; set; }
 */
        public string ServiceId { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        [ForeignKey("CreatedById")]
        public ApplicationUser CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public ApplicationUser UpdatedBy { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public static object StatusEnum { get; internal set; }
        public int TypeItemId { get; set; }
        [ForeignKey("TypeItemId")]
        public virtual TypeItem TypeItem { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Required]
        [DisplayName("Sender Name ")]
        public String SenderName { get; set; }

        [Required]
        [DisplayName("From Province ")]
        public int SenderProvinceID { get; set; }
        [ForeignKey("SenderProvinceID")]
        public virtual Province SenderProvince { get; set; }

        [Required]
        [DisplayName(" At Post Office ")]
        public string SenderOfficeID { get; set; }
        [ForeignKey("SenderOfficeID")]
        public virtual Office SenderOffice { get; set; }



        [Required]
        [StringLength(50)]
        [DisplayName(" Address Details")]
        public String SenderAddress { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [DisplayName("Sender PhoneNumber ")]
        public String SenderPhone { get; set; }



        [Required]
        [StringLength(50)]
        [DisplayName("Receiver Name ")]
        public String ReceiverName { get; set; }

        [Required]
        public int ReceiverProvinceID { get; set; }
        [ForeignKey("ReceiverProvinceID")]
        [DisplayName("To Province ")]
        public virtual Province ReceiverProvince { get; set; }

        [Required]
        [DisplayName("At Post Office ")]
        public string ReceiverOfficeID { get; set; }

        [ForeignKey("ReceiverOfficeID")]
        public virtual Office ReceiverOffice { get; set; }


        [Required]
        [DisplayName("Address Details Receiver ")]
        public String ReceiverAddress { get; set; }


        [Required]
        [DisplayName("PhoneNumber Receiver ")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public String ReceiverPhone { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Distance { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [DisplayName("Sent Date")]
        public DateTime? CreateAt { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price can't < 0")]
        public double PriceShip { get; set; }


        [Required]
        [DisplayName("Type Item ")]
        public int TypeItemId { get; set; }

        [ForeignKey("TypeItemId")]
        public virtual TypeItem TypeItem { get; set; }

        public EnumOrderStatus Status { get; set; }
        public enum EnumOrderStatus
        {
            New = -1,
            Packaging = 0,
            ShippingToOffice = 1,
            ShippingToHouse = 2,
            Shipped = 3,
            Finished = 4,
            Cancelled = 5,
            Deleted = 6
        }

        [Required]
        [DisplayName("Type Service ")]
        public string ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }


        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public ApplicationUser CreatedBy { get; set; }



        public string UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public ApplicationUser UpdatedBy { get; set; }

    }

   
}
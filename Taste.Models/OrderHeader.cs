using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taste.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey(name: "UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        [Display(Name ="Order Date")]
        public DateTime OrderDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }
        [Required]
        [Display(Name ="Pickup Time")]
        public DateTime PickupTime { get; set; }

        [NotMapped]
        [Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string Comments { get; set; }
        [Display(Name ="Pickup Name")]
        public string PickupName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string TransactionId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Entities
{
    public class OrderHeader
    {   
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double TotalOrderPrice { get; set; }
        [Required]
        [MaxLength(40)]
        public string ShippingName { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTimeOffset OrderDate { get; set; }
        public string CoupounCode { get; set; }
        public double CouponCodeDiscount { get; set; }
        public string Comments { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Entities
{
    public class OrderDetails
    {   
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; } = 1;
    }
}

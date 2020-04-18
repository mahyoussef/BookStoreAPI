using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Entities
{
    public class ApplicationUserCategories
    {   
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

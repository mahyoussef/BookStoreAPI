using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Entities
{
    public class BookCategories
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

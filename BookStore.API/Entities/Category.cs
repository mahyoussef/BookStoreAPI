using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<ApplicationUserCategories> ApplicationUserCategories { get; set; } 
        public IList<BookCategories> BookCategories { get; set; } 
    }
}

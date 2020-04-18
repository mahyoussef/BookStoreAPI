using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.API.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        [Required]
        public int TotalNumberOfPages { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public IList<BookCategories> BookCategories { get; set; }

    }
}

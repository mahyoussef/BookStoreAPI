using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Entities
{
    public class UserRole
    {   
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}

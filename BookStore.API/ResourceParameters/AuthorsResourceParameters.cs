using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.ResourceParameters
{
    public class AuthorsResourceParameters : PaginationParameters
    {
        public string SearchQuery { get; set; }
        public string OrderBy { get; set; } = "Name";
        public string Fields { get; set; } // For Shaping Data
    }
}

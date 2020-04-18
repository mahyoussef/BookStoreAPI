using AutoMapper;
using BookStore.API.ResourceParameters;
using BookStore.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/books")]
    public class BooksForAuthorsController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BooksForAuthorsController(IBookRepository bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository
                ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet("", Name = "GetBooksForAuthor")]
        [Produces("application/json")]
        public async Task<IActionResult> GetBooks(Guid authorId,
           [FromQuery] BooksResourceParameters booksResourceParameters)
        {
            var books = await _bookRepository.GetBooksForAuthorAsync(authorId,
                booksResourceParameters);
            return Ok(books);
        }
    }
}

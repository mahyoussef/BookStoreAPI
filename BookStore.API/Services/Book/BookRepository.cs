using BookStore.API.DbContexts;
using BookStore.API.Entities;
using BookStore.API.Extensions;
using BookStore.API.Models.BookDtos;
using BookStore.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Services
{
    public class BookRepository : IBookRepository, IDisposable
    {
        private BookStoreContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        public BookRepository(BookStoreContext context,
             IPropertyMappingService propertyMappingService)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService
                ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public void AddBook(Guid authorId, Book book)
        {
            if (authorId == null)
                throw new ArgumentNullException(nameof(authorId));

            if (book == null)
                throw new ArgumentNullException(nameof(book));

            book.AuthorId = authorId;
            _context.Books.Add(book);
        }

        public void AddCollectionOfBooks(Guid authorId, IEnumerable<Book> books)
        {
            throw new NotImplementedException();
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _context.Books.Remove(book);
        }

        public async Task<PagedList<Book>> GetBooksForAuthorAsync(Guid authorId, BooksResourceParameters booksResourceParameters)
        {
            if (booksResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(booksResourceParameters));
            }

            if (authorId == null)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            var collection = _context.Books as IQueryable<Book>;

            // get property mapping dictionary
            var bookPropertyMappingDictionary =
                _propertyMappingService.GetPropertyMapping<BookDto, Book>();

            // Apply Searching ..
            if (!string.IsNullOrWhiteSpace(booksResourceParameters.SearchQuery))
            {
                var searchQuery = booksResourceParameters.SearchQuery.Trim();
                collection = collection.Include(b => b.Author)
                    .Include(b => b.Publisher)
                    .Include(b => b.BookCategories)
                    .Where(b => b.Description.Contains(searchQuery)
                    || b.ISBN.Contains(searchQuery)
                    || b.Publisher.Name.Contains(searchQuery)
                    || b.Author.FirstName.Contains(searchQuery)
                    || b.Author.LastName.Contains(searchQuery)
                    || b.Title.Contains(searchQuery)
                    || b.BookCategories.Select(bc => bc.Category.Name).Contains(searchQuery));
            }

            var finalCollectopn = collection.Include(c => c.Author)
                                   .Include(c => c.Publisher)
                                   .Include(c => c.BookCategories);

            return await PagedList<Book>.Create(finalCollectopn,
                booksResourceParameters.PageNumber,
                booksResourceParameters.PageSize);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void UpdateBook(Guid authorId, Book book)
        {
            // No Implementation is here ...
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            }
        }
    }
}


using BookStore.API.DbContexts;
using BookStore.API.Entities;
using BookStore.API.Extensions;
using BookStore.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Services
{
    public class AuthorRepository : IAuthorRepository, IDisposable
    {
        private BookStoreContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        public AuthorRepository(BookStoreContext context,
            IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
            _propertyMappingService = propertyMappingService;
        }
        public void AddAuthor(Author author)
        {
            if(author == null)
                throw new NullReferenceException(nameof(author));

            _context.Authors.Add(author);
        }
        public void DeleteAuthor(Author author)
        {
            if (author == null)
                throw new NullReferenceException(nameof(author));

            _context.Authors.Remove(author);
        }
        public async Task<Author> GetAuthorAsync(Guid authorId)
        {
            if (authorId == null)
                throw new NullReferenceException(nameof(authorId));

            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
            return author;
        }

        public async Task<PagedList<Author>> GetAuthorsAsync(AuthorsResourceParameters authorsResourceParameters)
        {
            if (authorsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(authorsResourceParameters));
            }

            var collection = _context.Authors as IQueryable<Author>;

            // get property mapping dictionary
            var authorPropertyMappingDictionary =
                _propertyMappingService.GetPropertyMapping<Models.AuthorDto, Author>();

            // Apply Searching ..
            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.SearchQuery))
            {
                var searchQuery = authorsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.City.Contains(searchQuery)
                    || a.FirstName.Contains(searchQuery)
                    || a.LastName.Contains(searchQuery));
            }
            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.OrderBy))
            {
                
                collection = collection.ApplySort(authorsResourceParameters.OrderBy,
                    authorPropertyMappingDictionary);
            }
            return await PagedList<Author>.Create(collection,
                authorsResourceParameters.PageNumber,
                authorsResourceParameters.PageSize);
        }
        public void UpdateAuthor(Author author)
        {
            // No Implementation Code here due to Tracking Chaning in Context implicitly 
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
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

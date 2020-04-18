using BookStore.API.Entities;
using BookStore.API.Extensions;
using BookStore.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Services
{
    public interface IBookRepository
    {
        Task<PagedList<Book>> GetBooksForAuthorAsync(Guid authorId, BooksResourceParameters booksResourceParameters);
        void AddBook(Guid authorId, Book book);
        void AddCollectionOfBooks(Guid authorId, IEnumerable<Book> books);
        void UpdateBook(Guid authorId, Book book);
        void RemoveBook(Book book);
        Task<bool> SaveChangesAsync();
    }

  
}
using BookStore.API.Entities;
using BookStore.API.Extensions;
using BookStore.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Services
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthorAsync(Guid authorId);
        Task<PagedList<Author>> GetAuthorsAsync(AuthorsResourceParameters authorsResourceParameters);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
        Task<bool> SaveChangesAsync();
    }
}
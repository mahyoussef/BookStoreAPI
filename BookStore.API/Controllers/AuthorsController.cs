using AutoMapper;
using BookStore.API.Filters;
using BookStore.API.Models;
using BookStore.API.Models.AuthorDtos;
using BookStore.API.ResourceParameters;
using BookStore.API.Services;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;
        public AuthorsController(IAuthorRepository authorRepository,
            IMapper mapper, IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService)
        {
            _authorRepository = authorRepository 
                ?? throw new ArgumentNullException(nameof(authorRepository));
            _mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
             throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet("", Name = "GetAuthors")]
        [Produces("application/xml","application/json","application/vnd.marvin.hateoas+json")]
        public async Task<IActionResult> GetAuthors(
           [FromQuery] AuthorsResourceParameters authorsResourceParameters,
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }
            if (!_propertyMappingService.ValidMappingExistsFor<AuthorDto, Entities.Author>
                (authorsResourceParameters.OrderBy))
            {
                return BadRequest();
            }
            if (!_propertyCheckerService.TypeHasProperties<AuthorDto>
              (authorsResourceParameters.Fields))
            {
                return BadRequest();
            }
            var authorsFromRepo = await _authorRepository.GetAuthorsAsync(authorsResourceParameters);

            var paginationMetadata = new
            {
                totalCount = authorsFromRepo.TotalCount,
                pageSize = authorsFromRepo.PageSize,
                currentPage = authorsFromRepo.CurrentPage,
                totalPages = authorsFromRepo.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            var mappedAuthors = _mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);
            var shapedAuthors = mappedAuthors.ShapeData(authorsResourceParameters.Fields);

            if (parsedMediaType.MediaType.Contains("hateoas"))
            {
                var links = CreateLinksForAuthors(authorsResourceParameters,
                authorsFromRepo.HasNext,
                authorsFromRepo.HasPrevious);

                var shapedAuthorsWithLinks = shapedAuthors.Select(author =>
                {
                    var authorAsDictionary = author as IDictionary<string, object>;
                    var authorLinks = CreateLinksForAuthor((Guid)authorAsDictionary["Id"], null);
                    authorAsDictionary.Add("links", authorLinks);
                    return authorAsDictionary;
                });

                var linkedCollectionResource = new
                {
                    value = shapedAuthorsWithLinks,
                    links
                };

                return Ok(linkedCollectionResource);
            }
            else
            {
                return Ok(shapedAuthors);
            }
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        [Produces("application/json",
            "application/vnd.marvin.hateoas+json")]
        public async Task<IActionResult> GetAuthor(Guid authorId, string fields,
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType,
               out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<AuthorDto>
               (fields))
            {
                return BadRequest();
            }

            var authorFromRepo = await _authorRepository.GetAuthorAsync(authorId);
            if (authorFromRepo == null)
                return NotFound();

            var shapedAuthor = _mapper.Map<AuthorDto>(authorFromRepo)
                                   .ShapeData(fields);

            if (parsedMediaType.MediaType.Contains("Hateous"))
            {
                var links = CreateLinksForAuthor(authorFromRepo.Id ,fields);
                var authorAsDictionary = shapedAuthor as IDictionary<string, object>;
                var authorLinks = CreateLinksForAuthor((Guid)authorAsDictionary["Id"], null);
                authorAsDictionary.Add("links", authorLinks);
               
                var linkedResource = new
                {
                    value = authorAsDictionary,
                    links
                };

                return Ok(linkedResource);
            }
            else
            {
                return Ok(shapedAuthor);
            }
        }
        [HttpPost("", Name = "CreateAuthor")]
        [Consumes("application/json",
            "application/vnd.marvin.authorforcreation+json")]
        public async Task<IActionResult> CreateAuthor(
            [FromBody] AuthorForCreationDto authorForCreationDto)
        {
            if (authorForCreationDto == null)
                return BadRequest();

            var author = _mapper.Map<Entities.Author>(authorForCreationDto);
            _authorRepository.AddAuthor(author);
            await _authorRepository.SaveChangesAsync();

            var authorToReturn = _mapper.Map<Models.AuthorDto>(author);

            return CreatedAtRoute("GetAuthor",
                new { authorId = authorToReturn.Id },
                authorToReturn);
        }
        [HttpDelete("{authorId}", Name = "DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(Guid authorId)
        {
            var author = await _authorRepository.GetAuthorAsync(authorId);
            if (author == null)
                return NotFound();

            _authorRepository.DeleteAuthor(author);
            await _authorRepository.SaveChangesAsync();
            
            return NoContent();
        }
        [HttpPut("{authorId}", Name = "UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(Guid authorId,
            [FromBody] AuthorForUpdateDto authorForUpdateDto)
        {
            var authorFromRepo = await _authorRepository.GetAuthorAsync(authorId);
            if (authorFromRepo == null)
                return NotFound();

            _mapper.Map(authorForUpdateDto, authorFromRepo);
            _authorRepository.UpdateAuthor(authorFromRepo);
            await _authorRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{authorId}", Name = "UpdateAuthor")]
        public async Task<IActionResult> PartiallyUpdateCourseForAuthor(Guid authorId,
            JsonPatchDocument<AuthorForUpdateDto> patchDocument)
        {
            var authorFromRepo = await _authorRepository.GetAuthorAsync(authorId);
            if (authorFromRepo == null)
                return NotFound();

            var authorToPatch = _mapper.Map<AuthorForUpdateDto>(authorFromRepo);
            // add validation
            patchDocument.ApplyTo(authorToPatch, ModelState);
            if (!TryValidateModel(authorToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(authorToPatch, authorFromRepo);
            _authorRepository.UpdateAuthor(authorFromRepo);

            await _authorRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT,PATCH,DELETE");
            return Ok();
        }
        private string CreateAuthorsResourceUri(
           AuthorsResourceParameters authorsResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAuthors",
                      new
                      {
                          fields = authorsResourceParameters.Fields,
                          orderBy = authorsResourceParameters.OrderBy,
                          pageNumber = authorsResourceParameters.PageNumber - 1,
                          pageSize = authorsResourceParameters.PageSize,
                          searchQuery = authorsResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAuthors",
                      new
                      {
                          fields = authorsResourceParameters.Fields,
                          orderBy = authorsResourceParameters.OrderBy,
                          pageNumber = authorsResourceParameters.PageNumber + 1,
                          pageSize = authorsResourceParameters.PageSize,
                          searchQuery = authorsResourceParameters.SearchQuery
                      });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetAuthors",
                    new
                    {
                        fields = authorsResourceParameters.Fields,
                        orderBy = authorsResourceParameters.OrderBy,
                        pageNumber = authorsResourceParameters.PageNumber,
                        pageSize = authorsResourceParameters.PageSize,
                        searchQuery = authorsResourceParameters.SearchQuery
                    });
            }

        }

        private IEnumerable<LinkDto> CreateLinksForAuthor(Guid authorId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(Url.Link("GetAuthor", new { authorId }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(Url.Link("GetAuthor", new { authorId, fields }),
                  "self",
                  "GET"));
            }

            links.Add(
               new LinkDto(Url.Link("DeleteAuthor", new { authorId }),
               "delete_author",
               "DELETE"));

            links.Add(
                new LinkDto(Url.Link("CreateBookForAuthor", new { authorId }),
                "create_book_for_author",
                "POST"));

            links.Add(
               new LinkDto(Url.Link("GetBooksForAuthor", new { authorId }),
               "books",
               "GET"));

            return links;
        }
        private IEnumerable<LinkDto> CreateLinksForAuthors(
            AuthorsResourceParameters authorsResourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();
            // self 
            links.Add(
               new LinkDto(CreateAuthorsResourceUri(
                   authorsResourceParameters, ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateAuthorsResourceUri(
                      authorsResourceParameters, ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateAuthorsResourceUri(
                        authorsResourceParameters, ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }
    }
}

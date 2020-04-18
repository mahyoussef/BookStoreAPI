using AutoMapper;
using BookStore.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Entities.Author, Models.AuthorDto>()
               .ForMember(
               dest => dest.Name,
               opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
               .ForMember(
               dest => dest.Age,
               opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
               );

            CreateMap<Models.AuthorDtos.AuthorForCreationDto, Entities.Author>();
            CreateMap<Models.AuthorDtos.AuthorForUpdateDto, Entities.Author>();
            CreateMap<Entities.Author, Models.AuthorDtos.AuthorForUpdateDto>();
        }
    }
}

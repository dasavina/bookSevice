using AutoMapper;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Author mappings
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();

            // Book mappings
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id));

            CreateMap<Book, BookDetailedDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Publishers, opt => opt.MapFrom(src => src.BookPublishers.Select(bp => bp.Publisher)));

            // Publisher mappings
            CreateMap<Publisher, PublisherDto>();
        }
    }

}

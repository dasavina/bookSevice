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

            CreateMap<Author, AuthorWithBooksDTO>();
            CreateMap<AuthorWithBooksDTO, Author>();

            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id));

            CreateMap<Book, DetailedBookDTO>()
                        .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                        .ForMember(dest => dest.PublisherNames, opt => opt.MapFrom(src => src.BookPublishers.Select(bp => bp.Publisher.Name)));

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id));
                

            CreateMap<BookDto, BookPublisherDto>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id));
            CreateMap<BookPublisherDto, BookDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BookId));

            CreateMap<PublisherDto, BookPublisherDto>()
               .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.Id));
            CreateMap<BookPublisherDto, PublisherDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublisherId));

            CreateMap<BookPublisher, BookPublisherDto>();
            CreateMap<BookPublisherDto, BookPublisher>();

            CreateMap<Book, BookPublisherDto>();
            CreateMap<BookPublisherDto, Book>();

            // Publisher mappings
            CreateMap<Publisher, PublisherDto>();
            CreateMap<PublisherDto, Publisher>();

        }
    }

}

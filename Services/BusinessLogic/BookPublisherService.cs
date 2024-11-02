using AutoMapper;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessLogic
{
    public class BookPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookPublisherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddPublisherToBookAsync(BookPublisherDto dto)
        {
            await _unitOfWork.BookPublishers.AddPublisherToBookAsync(dto.BookId, dto.PublisherId);
        }

        public async Task<IEnumerable<PublisherDto>> GetPublishersByBookIdAsync(int bookId)
        {
            var publishers = await _unitOfWork.BookPublishers.GetPublishersByBookIdAsync(bookId);
            return _mapper.Map<IEnumerable<PublisherDto>>(publishers);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByPublisherIdAsync(int publisherId)
        {
            var books = await _unitOfWork.BookPublishers.GetBooksByPublisherIdAsync(publisherId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

    }

}

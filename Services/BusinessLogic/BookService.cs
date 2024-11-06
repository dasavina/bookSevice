using AutoMapper;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Models.DTOs;
using Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessLogic
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IDistributedCache _distributedCache;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }


        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return (bookDtos);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> CreateBookAsync([FromBody] BookDto bookDto)
        {
            
            var book = _mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> UpdateBookAsync(int id, [FromBody] BookDto bookDto)
        {
            var existingBook = await _unitOfWork.Books.GetByIdAsync(id);
            if (existingBook == null)
                return false;

            _mapper.Map(bookDto, existingBook);
            _unitOfWork.Books.UpdateAsync(existingBook);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return false;
            await _unitOfWork.Books.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return true;
        }
        public async Task<BookDto> GetBookWithAuthorAsync(int id)
        {
            var book = await _unitOfWork.Books.GetBookWithAuthorAsync(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> GetBookByIdCachedAsync(int id)
        {
            var cacheKey = $"Book_{id}";
            var cachedBook = await _distributedCache.GetStringAsync(cacheKey);

            if (cachedBook != null)
            {
                return JsonConvert.DeserializeObject<BookDto>(cachedBook);
            }
            else
            {
                var book = await _unitOfWork.Books.GetByIdAsync(id);
                var bookDto = _mapper.Map<BookDto>(book);

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), // Absolute Expiration
                    SlidingExpiration = TimeSpan.FromMinutes(5) // Sliding Expiration
                };

                await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(bookDto), cacheOptions);

                return bookDto;
            }
        }


        public async Task<DetailedBookDTO> GetFullInfoAsync(int bookId)
        {
            var book = await _unitOfWork.Books.GetBookWithDetailsAsync(bookId);
            if (book == null) return null;

            var detailedBookDto = _mapper.Map<DetailedBookDTO>(book);
            return detailedBookDto;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.SaveAsync();
        }
       
        public async Task<IEnumerable<BookDto>> GetBooksFilteredPagedSorted(string titleFilter = null, string sortBy = null, int page = 1, int pageSize = 10)
        {
            var books = await _unitOfWork.Books.GetBooksAsync(titleFilter, sortBy, page, pageSize);
            return books.Select(b => _mapper.Map<BookDto>(b));
        }


    }

}

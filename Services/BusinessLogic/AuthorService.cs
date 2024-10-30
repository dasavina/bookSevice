using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Repositories.Interfaces;
    using Models.DTOs;
    using Models.Entities;

namespace Services.BusinessLogic
{

    
        public class AuthorService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
            {
                var authors = await _unitOfWork.Authors.GetAllAsync();
                return _mapper.Map<IEnumerable<AuthorDto>>(authors);
            }

            public async Task<AuthorDto> GetAuthorByIdAsync(int id)
            {
                var author = await _unitOfWork.Authors.GetByIdAsync(id);
                return _mapper.Map<AuthorDto>(author);
            }

            public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto)
            {
                var author = _mapper.Map<Author>(authorDto);
                await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<AuthorDto>(author);
            }

            public async Task<bool> UpdateAuthorAsync(int id, AuthorDto authorDto)
            {
                var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(id);
                if (existingAuthor == null)
                    return false;

                _mapper.Map(authorDto, existingAuthor);
                _unitOfWork.Authors.UpdateAsync(existingAuthor);
                await _unitOfWork.SaveAsync();
                return true;
            }

            public async Task<bool> DeleteAuthorAsync(int id)
            {
                var author = await _unitOfWork.Authors.GetByIdAsync(id);
                if (author == null)
                    return false;

                _unitOfWork.Authors.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }


}

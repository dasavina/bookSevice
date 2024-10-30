using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BusinessLogic
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Repositories.Interfaces;
    using Models.DTOs;
    using Models.Entities;

    namespace YourProject.Services
    {
        public class PublisherService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<PublisherDto>> GetAllPublishersAsync()
            {
                var publishers = await _unitOfWork.Publishers.GetAllAsync();
                return _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            }

            public async Task<PublisherDto> GetPublisherByIdAsync(int id)
            {
                var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
                return _mapper.Map<PublisherDto>(publisher);
            }

            public async Task<PublisherDto> CreatePublisherAsync(PublisherDto publisherDto)
            {
                var publisher = _mapper.Map<Publisher>(publisherDto);
                await _unitOfWork.Publishers.AddAsync(publisher);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<PublisherDto>(publisher);
            }

            public async Task<bool> UpdatePublisherAsync(int id, PublisherDto publisherDto)
            {
                var existingPublisher = await _unitOfWork.Publishers.GetByIdAsync(id);
                if (existingPublisher == null)
                    return false;

                _mapper.Map(publisherDto, existingPublisher);
                _unitOfWork.Publishers.UpdateAsync(existingPublisher);
                await _unitOfWork.SaveAsync();
                return true;
            }

            public async Task<bool> DeletePublisherAsync(int id)
            {
                var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
                if (publisher == null)
                    return false;

                _unitOfWork.Publishers.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }
    }

}

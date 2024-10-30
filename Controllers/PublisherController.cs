using AutoMapper;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublisherController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Publisher
        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            var publishers = await _unitOfWork.Publishers.GetAllAsync();
            var publisherDtos = _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            return Ok(publisherDtos);
        }

        // GET: api/Publisher/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            var publisherDto = _mapper.Map<PublisherDto>(publisher);
            return Ok(publisherDto);
        }

        // POST: api/Publisher
        [HttpPost]
        public async Task<IActionResult> CreatePublisher([FromBody] PublisherDto publisherCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = _mapper.Map<Publisher>(publisherCreateDto);
            await _unitOfWork.Publishers.AddAsync(publisher);
            await _unitOfWork.SaveAsync();

            var publisherDto = _mapper.Map<PublisherDto>(publisher);
            return CreatedAtAction(nameof(GetPublisher), new { id = publisher.Id }, publisherDto);
        }

        // PUT: api/Publisher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherDto publisherUpdateDto)
        {
            if (id != publisherUpdateDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _mapper.Map(publisherUpdateDto, publisher);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Publisher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            await _unitOfWork.Publishers.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

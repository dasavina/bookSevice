using AutoMapper;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;
using Services.BusinessLogic.YourProject.Services;
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
        private readonly PublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(PublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        // GET: api/Publisher
        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            var publisherDtos = _mapper.Map<IEnumerable<PublisherDto>>(publishers);
            return Ok(publisherDtos);
        }

        // GET: api/Publisher/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisher = await _publisherService.GetPublisherByIdAsync(id);
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

            var createdPublisherDto = await _publisherService.CreatePublisherAsync(publisherCreateDto);
            return CreatedAtAction(nameof(GetPublisher), new { id = createdPublisherDto.Id }, createdPublisherDto);
        }

        // PUT: api/Publisher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherDto publisherUpdateDto)
        {
            if (id != publisherUpdateDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var isUpdated = await _publisherService.UpdatePublisherAsync(id, publisherUpdateDto);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Publisher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var isDeleted = await _publisherService.DeletePublisherAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

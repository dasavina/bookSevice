using AutoMapper;
using BLL.BusinessLogic;
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
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(AuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorDtos);
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorWithBooksAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var authorDto = _mapper.Map<AuthorWithBooksDTO>(author);
            return Ok(authorDto);
        }

        // POST: api/Author
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAuthorDto = await _authorService.CreateAuthorAsync(authorDto);

            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthorDto.Id }, createdAuthorDto);
        }
    }

}

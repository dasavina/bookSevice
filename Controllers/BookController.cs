using AutoMapper;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;
using Services.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    
        [ApiController]
        [Route("api/[controller]")]
        public class BookController : ControllerBase
        {
            private readonly BookService _bookService;
            private readonly IMapper _mapper;

            public BookController(BookService bookService, IMapper mapper)
            {
                _bookService = bookService;
                _mapper = mapper;
            }

            // GET: api/Book
            [HttpGet]
            public async Task<IActionResult> GetBooks()
            {
                var bookDtos = await _bookService.GetAllBooksAsync();
                return Ok(bookDtos);
            }

            // GET: api/Book/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetBook(int id)
            {
                var book = await _bookService.GetBookWithAuthorAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                var bookDto = _mapper.Map<BookDetailedDto>(book);
                return Ok(bookDto);
            }

            // POST: api/Book
            [HttpPost]
            public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdBookDto = await _bookService.CreateBookAsync(bookDto);

                return CreatedAtAction(nameof(GetBook), new { id = createdBookDto.Id }, createdBookDto);
            }

            // PUT: api/Book/5
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
            {
                if (id != bookDto.Id || !ModelState.IsValid)
                {
                    return BadRequest();
                }

                var updated = await _bookService.UpdateBookAsync(id, bookDto);
                if (!updated)
                {
                    return NotFound();
                }

                return NoContent();
            }

            // DELETE: api/Book/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteBook(int id)
            {
                var deleted = await _bookService.DeleteBookAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }

            // GET: api/Book with filter, sort, and pagination
            [HttpGet("filter")]
            public async Task<IActionResult> GetBooks(
                [FromQuery] string titleFilter,
                [FromQuery] string sortBy,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10)
            {
                var books = await _bookService.GetBooksFilteredPagedSorted(titleFilter, sortBy, page, pageSize);
                return Ok(books);
            }
        }
    


}

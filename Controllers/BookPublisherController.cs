using BLL.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookPublisherController : ControllerBase
    {
        private readonly BookPublisherService _bookPublisherService;

        public BookPublisherController(BookPublisherService bookPublisherService)
        {
            _bookPublisherService = bookPublisherService;
        }

        [HttpPost("AddPublisherToBook")]
        public async Task<IActionResult> AddPublisherToBook([FromBody] BookPublisherDto dto)
        {
            await _bookPublisherService.AddPublisherToBookAsync(dto);
            return Ok();
        }

        [HttpGet("{bookId}/publishers")]
        public async Task<IActionResult> GetPublishersByBookId(int bookId)
        {
            var publishers = await _bookPublisherService.GetPublishersByBookIdAsync(bookId);
            return Ok(publishers);
        }

        [HttpGet("publisher/{publisherId}/books")]
        public async Task<IActionResult> GetBooksByPublisherId(int publisherId)
        {
            var books = await _bookPublisherService.GetBooksByPublisherIdAsync(publisherId);
            return Ok(books);
        }
    }

}

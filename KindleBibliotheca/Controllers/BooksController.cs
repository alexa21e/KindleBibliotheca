
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using KindleBibliotheca.DTOs;
using KindleBibliotheca.Errors;
using KindleBibliotheca.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;

namespace KindleBibliotheca.Controllers
{
    public class BooksController : BaseAPIController
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return list of all books")]
        public async Task<ActionResult<Pagination<BookToReturn>>> GetBooks(
           [FromQuery]BookSpecParam bookParams)
        {
            var books = await _booksService.GetBooks(bookParams); 
            var totalBooks = await _booksService.GetBooksCount(bookParams);
            return Ok(new Pagination<BookToReturn>(bookParams.PageIndex,
                bookParams.PageSize, totalBooks, books));
        }
    
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return book by id")]
        public async Task<ActionResult<BookToReturn>> GetBook(Guid id)
        {
            var book = await _booksService.GetBook(id);
            if (book == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return book;
        }

        [HttpPost("new"),DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Create a book")]
        public async Task<ActionResult<Book>> CreateBook([FromBody] BookToCreate bookToCreate)
        {
            try
            {
                var book = await _booksService.CreateBook(bookToCreate);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
    }
}


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
using Swashbuckle.AspNetCore.Annotations;

namespace KindleBibliotheca.Controllers
{
    public class BooksController : BaseAPIController
    {
        private readonly IGenericRepository<Book> _booksRepo;
        private readonly IGenericRepository<Series> _seriesRepo;
        private readonly IMapper _mapper;

        public BooksController(IGenericRepository<Book> booksRepo, IGenericRepository<Series> seriesRepo,
            IMapper mapper)
        {
            _mapper = mapper;
            _booksRepo = booksRepo;
            _seriesRepo = seriesRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return list of all books")]
        public async Task<ActionResult<Pagination<BookToReturn>>> GetBooks(
           [FromQuery]BookSpecParam bookParams)
        {
            var spec = new BooksWithSeriesSpecifications(bookParams);
            var countSpec = new BookWithFiltersForCountSpecification(bookParams);
            var totalBooks = await _booksRepo.CountAsync(spec);
            var books = await _booksRepo.ListAsync(spec);
            var data = _mapper
                .Map<IReadOnlyList<Book>, IReadOnlyList<BookToReturn>>(books);
            return Ok(new Pagination<BookToReturn>(bookParams.PageIndex,
                bookParams.PageSize, totalBooks, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return book by id")]
        public async Task<ActionResult<BookToReturn>> GetBook(Guid id)
        {
            var spec = new BooksWithSeriesSpecifications(id);
            var book = await _booksRepo.GetEntityWithSpec(spec);
            if (book == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Book, BookToReturn>(book);
        }

        [HttpGet("series")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return list of all series")]
        public async Task<ActionResult<List<Series>>> GetSeries()
        {
            return Ok(await _seriesRepo.ListAllAsync());
        }

    }
}

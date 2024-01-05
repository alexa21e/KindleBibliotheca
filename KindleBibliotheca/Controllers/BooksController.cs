
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using KindleBibliotheca.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KindleBibliotheca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
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
        public async Task<ActionResult<List<BookToReturn>>> GetBooks()
        {
            var spec = new BooksWithSeriesSpecifications();
            var books = await _booksRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookToReturn>>(books));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookToReturn>> GetBook(Guid id)
        {
            var spec = new BooksWithSeriesSpecifications(id);
            var book = await _booksRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Book, BookToReturn>(book);
        }

        [HttpGet("series")]
        public async Task<ActionResult<List<Series>>> GetSeries()
        {
            return Ok(await _seriesRepo.ListAllAsync());
        }

    }
}

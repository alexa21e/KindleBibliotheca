
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

        public BooksController(IGenericRepository<Book> booksRepo, IGenericRepository<Series> seriesRepo)
        {
            _booksRepo = booksRepo;
            _seriesRepo = seriesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookToReturn>>> GetBooks()
        {
            var spec = new BooksWithSeriesSpecifications();
            var books = await _booksRepo.ListAsync(spec);
            return books.Select(book => new BookToReturn
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishingDate = book.PublishingDate,
                Rating = book.Rating,
                Genre = book.Genre,
                PublishingHouse = book.PublishingHouse,
                SeriesName = book.Series.Name,
                SeriesPlace = book.SeriesPlace,
                PagesNumber = book.PagesNumber,
                Description = book.Description,
                CoverUrl = book.CoverUrl
            }).ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookToReturn>> GetBook(Guid id)
        {
            var spec = new BooksWithSeriesSpecifications(id);
            var book = await _booksRepo.GetEntityWithSpec(spec);
            return new BookToReturn
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishingDate = book.PublishingDate,
                Rating = book.Rating,
                Genre = book.Genre,
                PublishingHouse = book.PublishingHouse,
                SeriesName = book.Series.Name,
                SeriesPlace = book.SeriesPlace,
                PagesNumber = book.PagesNumber,
                Description = book.Description,
                CoverUrl = book.CoverUrl
            };
        }

        [HttpGet("series")]
        public async Task<ActionResult<List<Series>>> GetSeries()
        {
            return Ok(await _seriesRepo.ListAllAsync());
        }

    }
}

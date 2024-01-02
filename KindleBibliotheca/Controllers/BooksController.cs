﻿
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
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
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var spec = new BooksWithSeriesSpecifications();
            var books = await _booksRepo.ListAsync(spec);
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            var spec = new BooksWithSeriesSpecifications(id);
            return await _booksRepo.GetEntityWithSpec(spec);
        }

        [HttpGet("series")]
        public async Task<ActionResult<List<Series>>> GetSeries()
        {
            return Ok(await _seriesRepo.ListAllAsync());
        }

    }
}

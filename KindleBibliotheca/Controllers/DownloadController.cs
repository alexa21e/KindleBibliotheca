using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using KindleBibliotheca.DTOs;
using KindleBibliotheca.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;

namespace KindleBibliotheca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IGenericRepository<Book> _booksRepo;
        private readonly IWebHostEnvironment environment;
        public DownloadController(IGenericRepository<Book> booksRepo,
            IWebHostEnvironment hostEnvironment)
        {
            _booksRepo = booksRepo;
            environment = hostEnvironment;
        }

        [HttpGet("pdf/{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Downloads the PDF format of a specific book")]
        public async Task<ActionResult> DownloadPDF(Guid bookId)
        {
            var spec = new BooksWithSeriesAndAuthorsSpecifications(bookId);
            var book = await _booksRepo.GetEntityWithSpec(spec);
            if (book == null)
            {
                return NotFound(new ApiResponse(404));
            }

            if (string.IsNullOrEmpty(book.PDFUrl))
            {
                return BadRequest("The PDF format of the book doesn't exist in the database");
            }

            var filePath = Path.Combine(environment.WebRootPath, book.PDFUrl);

            return File(System.IO.File.ReadAllBytes(filePath), "application/pdf", System.IO.Path.GetFileName(filePath));
        }
    }
}


using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KindleBibliotheca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IBookRepository _booksRepo;
        private readonly IWebHostEnvironment _environment;
        public DownloadController(IBookRepository booksRepo,
            IWebHostEnvironment hostEnvironment)
        {
            _booksRepo = booksRepo;
            _environment = hostEnvironment;
        }

        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> DownloadPDF(Guid id)
        {
            var book = await _booksRepo.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            if (string.IsNullOrEmpty(book.PDFUrl))
            {
                return BadRequest("The PDF format of the book doesn't exist in the database");
            }

            var filePath = Path.Combine(_environment.WebRootPath, book.PDFUrl);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pdf", Path.GetFileName(filePath));
        }
    }
}



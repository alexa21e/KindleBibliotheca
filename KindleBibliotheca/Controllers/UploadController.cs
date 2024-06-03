using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KindleBibliotheca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IBookRepository _booksRepo;
        private readonly IWebHostEnvironment _environment;

        public UploadController(IBookRepository booksRepo, IWebHostEnvironment environment)
        {
            _booksRepo = booksRepo;
            _environment = environment;
        }

        [HttpPost("cover/{id}")]
        public async Task<IActionResult> UploadCover(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var book = await _booksRepo.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            var folderName = Path.Combine("wwwroot", "images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            book.CoverUrl = Path.Combine("images", fileName);
            _booksRepo.Update(book);
            await _booksRepo.SaveAsync();

            return Ok(new { book.CoverUrl });
        }

        [HttpPost("pdf/{id}")]
        public async Task<IActionResult> UploadPDF(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var book = await _booksRepo.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            var folderName = Path.Combine("wwwroot", "PDF");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            book.PDFUrl = Path.Combine("PDF", fileName);
            _booksRepo.Update(book);
            await _booksRepo.SaveAsync();

            return Ok(new { book.PDFUrl });
        }
    }
}


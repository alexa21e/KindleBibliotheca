using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using KindleBibliotheca.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;

namespace KindleBibliotheca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IGenericRepository<Book> _booksRepo;
        private readonly IMapper _mapper;

        public UploadController(IGenericRepository<Book> booksRepo, IMapper mapper)
        {
            _mapper = mapper;
            _booksRepo = booksRepo;
        }
        [HttpPost("cover/{id}")]
        public async Task<IActionResult> UploadCover(Guid id)
        {
            var spec = new BooksWithSeriesAndAuthorsSpecifications(id);
            var book = await _booksRepo.GetEntityWithSpec(spec);
            try
            {
                var file = Request.Form.Files[0];
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }

                var folderName = Path.Combine("wwwroot", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                book.CoverUrl = Path.Combine("images", fileName);
                _booksRepo.Update(book);

                await _booksRepo.SaveAsync();

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
    }
}

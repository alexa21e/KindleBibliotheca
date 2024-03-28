
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
        private readonly IBookRepository _booksRepo;
        private readonly IAuthorRepository _authorsRepo;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository booksRepo, 
            IAuthorRepository authorsRepo, IMapper mapper)
        {
            _mapper = mapper;
            _booksRepo = booksRepo;
            _authorsRepo  = authorsRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return list of all books")]
        public async Task<ActionResult<Pagination<BookToReturn>>> GetBooks(
           [FromQuery]BookSpecParam bookParams)
        {
            var spec = new BooksWithSeriesAndAuthorsSpecifications(bookParams);
            var countSpec = new BooksWithFiltersForCountSpecification(bookParams);
            var totalBooks = await _booksRepo.CountAsync(countSpec);
            var books = await _booksRepo.GetBooksWithSpecAsync(spec);
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
            var spec = new BooksWithSeriesAndAuthorsSpecifications(id);
            var book = await _booksRepo.GetEntityWithSpec(spec);
            if (book == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Book, BookToReturn>(book);
        }

        [HttpPost("new"),DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Create a book")]
        public async Task<ActionResult<Book>> CreateBook([FromBody] BookToCreate bookToCreate)
        {
            try
            {
                var authorSpec = new AuthorsWithBooksSpecification();
                var authors = await _authorsRepo.GetAuthorsWithSpecAsync(authorSpec);
                var existingAuthor = authors.FirstOrDefault(a => a.Name == bookToCreate.AuthorName);
                if (existingAuthor == null)
                {
                    Author author = new Author()
                    {
                        Id = new Guid(),
                        Name = bookToCreate.AuthorName,
                        Books = new List<Book>()
                    };
                    var book = new Book()
                    {
                        Title = bookToCreate.Title,
                        Author = author,
                        AuthorId = author.Id,
                        CoverUrl = "",
                        Description = bookToCreate.Description,
                        Genre = bookToCreate.Genre,
                        Id = new Guid(),
                        PagesNumber = bookToCreate.PagesNumber,
                        PDFUrl = "",
                        PublishingDate = bookToCreate.PublishingDate,
                        Rating = bookToCreate.Rating,
                    };
                    author.Books.Add(book);

                    _booksRepo.Add(book);
                    _authorsRepo.Add(author);

                    await _booksRepo.SaveAsync();
                    await _authorsRepo.SaveAsync();

                    book.Author.Books = null;

                    return Ok(book);
                }
                else
                {
                    var book = new Book()
                    {
                        Title = bookToCreate.Title,
                        Author = existingAuthor,
                        AuthorId = existingAuthor.Id,
                        CoverUrl = "",
                        Description = bookToCreate.Description,
                        Genre = bookToCreate.Genre,
                        Id = new Guid(),
                        PagesNumber = bookToCreate.PagesNumber,
                        PDFUrl = "",
                        PublishingDate = bookToCreate.PublishingDate,
                        Rating = bookToCreate.Rating,
                    };
                    existingAuthor.Books.Add(book);

                    _booksRepo.Add(book);
                   await _booksRepo.SaveAsync();

                    book.Author.Books = null;

                    return Ok(book);
                }
                

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
    }
}

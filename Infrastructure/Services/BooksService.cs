using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using KindleBibliotheca.DTOs;

namespace Infrastructure.Services
{
    public class BooksService: IBooksService
    {
        private readonly IBookRepository _booksRepo;
        private readonly IAuthorRepository _authorsRepo;
        private readonly IMapper _mapper;
        public BooksService(IBookRepository booksRepo, IAuthorRepository authorsRepo, IMapper mapper)
        {
            _booksRepo = booksRepo;
            _authorsRepo = authorsRepo;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<Book>> GetBooks(BookSpecParam bookParams)
        {
            var spec = new BooksWithSeriesAndAuthorsSpecifications(bookParams);
            return await _booksRepo.GetBooksWithSpecAsync(spec);
        }
        public async Task<int> GetBooksCount(BookSpecParam bookParams)
        {
            var countSpec = new BooksWithFiltersForCountSpecification(bookParams);
            return await _booksRepo.CountAsync(countSpec);
        }
        public async Task<BookToReturn> GetBook(Guid id)
        {
            var bookSpec = new BooksWithSeriesAndAuthorsSpecifications(id);
            var book = await _booksRepo.GetEntityWithSpec(bookSpec);
            return _mapper.Map<Book, BookToReturn>(book);
        }
        public async Task<Book> CreateBook(BookToCreate bookToCreate)
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
                var book = _mapper.Map<Book>(bookToCreate, opts => opts.Items["Author"] = author);

                author.Books.Add(book);

                _booksRepo.Add(book);
                _authorsRepo.Add(author);

                await _booksRepo.SaveAsync();
                await _authorsRepo.SaveAsync();

                book.Author.Books = null;

                return book;
            }
            else
            {
                var book = _mapper.Map<Book>(bookToCreate);

                existingAuthor.Books.Add(book);

                _booksRepo.Add(book);
                await _booksRepo.SaveAsync();

                book.Author.Books = null;

                return book;
            }
        }
    }
}

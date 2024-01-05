using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KindleBibliotheca.Controllers
{
    public class BugController : BaseAPIController
    {
        private readonly BibliothecaContext _context;
        public BugController(BibliothecaContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNoFoundRequest()
        {
            var book = _context.Books.Find(new Guid("13d367dd-27a8-4456-8bf3-d597c692557a"));
            if (book == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var book = _context.Books.Find(43);
            var bookToReturn = book.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}


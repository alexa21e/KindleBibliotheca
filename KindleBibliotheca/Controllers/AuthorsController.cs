using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using KindleBibliotheca.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KindleBibliotheca.Controllers
{
    public class AuthorsController : BaseAPIController
    {
        private readonly IAuthorRepository _authorsRepo;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorsRepo, IMapper mapper)
        {
            _mapper = mapper;
            _authorsRepo = authorsRepo;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return list of all authors")]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            return Ok(await _authorsRepo.GetAuthorsAsync());
        }
    }
}

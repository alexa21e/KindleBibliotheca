using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using KindleBibliotheca.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KindleBibliotheca.Controllers
{
    public class SeriesController : BaseAPIController
    {
        private readonly ISeriesRepository _seriesRepo;
        private readonly IMapper _mapper;
        public SeriesController(ISeriesRepository seriesRepo, IMapper mapper)
        {
            _seriesRepo = seriesRepo;
            _mapper = mapper;
        }
        [HttpGet("series")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Return list of all series")]
        public async Task<ActionResult<List<Series>>> GetSeries()
        {
            return Ok(await _seriesRepo.GetSeriesAsync());
        }
    }
}

using AutoMapper;
using Library.IRepository;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork, ILogger<AuthorController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAuthors()
        {
            try
            {
                var authors = await _unitOfWork.Authors.GetAll();
                var results = _mapper.Map<IList<AuthorDTO>>(authors);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(GetAuthors)}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAuthor(int id)
        {
            try
            {
                var author = await _unitOfWork.Authors.Get(x => x.Id == id, new List<string> { "Books" });
                var result = _mapper.Map<AuthorDTO>(author);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(GetAuthor)}");
                return StatusCode(500);
            }
        }
    }
}

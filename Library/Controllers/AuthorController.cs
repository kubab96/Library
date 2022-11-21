using AutoMapper;
using Library.Data;
using Library.IRepository;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id:int}", Name = "GetAuthor")]
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

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateAuthor([FromBody] CreateAuthorDTO createAuthorDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid post attempt in {nameof(CreateAuthor)}");
                return BadRequest(ModelState);
            }
            try
            {
                var author = _mapper.Map<Author>(createAuthorDTO);
                await _unitOfWork.Authors.Insert(author);
                await _unitOfWork.Save();
                return CreatedAtRoute("GetAuthor", new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(CreateAuthor)}");
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] CreateAuthorDTO createAuthorDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid put attempt in {nameof(UpdateAuthor)}");
                return BadRequest(ModelState);
            }
            try
            {
                var author = await _unitOfWork.Authors.Get(x => x.Id == id);
                if (author == null)
                {
                    _logger.LogError($"Invalid put attempt in {nameof(UpdateAuthor)}");
                    return BadRequest("Invalid data");
                }
                _mapper.Map(createAuthorDTO, author);

                _unitOfWork.Authors.Update(author);
                await _unitOfWork.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(UpdateAuthor)}");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteAuthor)}");
                return BadRequest();
            }
            try
            {
                var author = await _unitOfWork.Authors.Get(x => x.Id == id);
                if (author == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteAuthor)}");
                    return BadRequest("Invalid data");
                }

                await _unitOfWork.Authors.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in {nameof(DeleteAuthor)}");
                return StatusCode(500);
            }
        }
    }
}

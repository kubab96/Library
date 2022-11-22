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
            var authors = await _unitOfWork.Authors.GetAll();
            var results = _mapper.Map<IList<AuthorDTO>>(authors);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAuthor(int id)
        {
            var author = await _unitOfWork.Authors.Get(x => x.Id == id, new List<string> { "Books" });
            var result = _mapper.Map<AuthorDTO>(author);
            return Ok(result);
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
            var author = _mapper.Map<Author>(createAuthorDTO);
            await _unitOfWork.Authors.Insert(author);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetAuthor", new { id = author.Id }, author);
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
    }
}

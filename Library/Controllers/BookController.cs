using AutoMapper;
using Library.Data;
using Library.IRepository;
using Library.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, ILogger<BookController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]        
        [HttpCacheValidation(MustRevalidate = false)]                                   
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBooks([FromQuery] RequestParams requestParams)
        {
            var books = await _unitOfWork.Books.GetPagedList(requestParams, new List<string> { "Authors" });
            var results = _mapper.Map<IList<BookDTO>>(books);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBook(int id)
        {
            var book = await _unitOfWork.Books.Get(x => x.Id == id, new List<string> { "Authors" });
            var result = _mapper.Map<BookDTO>(book);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateBook([FromBody] CreateBookDTO createBookDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateBook)}");
                return BadRequest(ModelState);
            }
            var book = _mapper.Map<Book>(createBookDTO);

            await _unitOfWork.Books.Insert(book);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] UpdateBookDTO updateBookDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid PUT attempt in {nameof(UpdateBook)}");
                return BadRequest(ModelState);
            }
            var book = await _unitOfWork.Books.Get(x => x.Id == id, new List<string> { "Authors" });
            if (book == null)
            {
                _logger.LogError($"Invalid PUT attempt in {nameof(UpdateBook)}");
                return BadRequest("Invalid data");
            }
            _mapper.Map(updateBookDTO, book);

            _unitOfWork.Books.Update(book);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBook(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteBook)}");
                return BadRequest();
            }
            var book = await _unitOfWork.Books.Get(x => x.Id == id);
            if (book == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteBook)}");
                return BadRequest("Invalid data");
            }

            await _unitOfWork.Books.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}

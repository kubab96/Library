using AutoMapper;
using Library.IRepository;
using Library.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBooks()
        {
            try
            {
                var books = await _unitOfWork.Books.GetAll();
                var results = _mapper.Map<IList<BookDTO>>(books);
                return Ok(results);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in {nameof(GetBooks)}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBook(int id)
        {
            try
            {
                var book = await _unitOfWork.Books.Get(x => x.Id == id, new List<string> { "Authors" });
                var result = _mapper.Map<BookDTO>(book);
                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something went wrong in {nameof(GetBook)}");
                return StatusCode(500);
            }
        }
    }
}

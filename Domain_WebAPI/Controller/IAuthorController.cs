using Domain_WebAPI.Data;
using Domain_WebAPI.Model.DTO;
using Domain_WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain_WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IAuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAuthorReponsitory _authorReponsitory;

        public IAuthorController(AppDbContext context, IAuthorReponsitory authorReponsitory)
        {
            _context = context;
            _authorReponsitory = authorReponsitory;
        }

        //GET ALL AUTHOR
        [HttpGet ("get-all-author")]

        public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool isAcending, [FromQuery] int numberPage = 1, int pageSize = 1000)
        {
            var allAuthors = _authorReponsitory.GetAllAuthor(filterOn, filterQuery, sortBy, isAcending, numberPage, pageSize);
            return Ok(allAuthors);
        }

        //GET AUTHOR BY ID

        [HttpGet]
        [Route("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id) 
        {
            var _authorById = _authorReponsitory.GetAuthorById(id);
            return Ok(_authorById);
        }

        //ADD AUTHOR
        [HttpPost("add-author")]
        public IActionResult AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var AuthorAdd = _authorReponsitory.AddAuthor(addAuthorRequestDTO);
            return Ok(AuthorAdd);
        }

        //UPDATE AUTHOR BY ID

        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] authorNoIdDTO updateAuthor)
        {
            var _updateAuthor = _authorReponsitory.UpdateAuthorById(id, updateAuthor);
            return Ok(_updateAuthor);
        }

        //DELETE AUTHOR BY ID

        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var deletedAuthor = _authorReponsitory.DeleteById(id);
            return Ok(deletedAuthor);
        }

    }
}

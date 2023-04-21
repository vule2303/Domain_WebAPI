using Domain_WebAPI.Data;
using Domain_WebAPI.Model.DTO;
using Domain_WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Domain_WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IBookController : ControllerBase
    {
        private readonly AppDbContext _DbContext;
        private readonly IBookReponsitory _bookReponsitory;

        public IBookController (AppDbContext dbContext, IBookReponsitory bookReponsitory)
        {
            _DbContext = dbContext;
            _bookReponsitory = bookReponsitory;
        }

        //GET ALL METHOD
        [HttpGet("get-all-books")]
        public IActionResult GetAll()
        {
            var allBooks = _bookReponsitory.GetAllBooks();
            return Ok(allBooks);
        }

        //GET BOOK BY ID
        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute]int id) 
        {
            var book = _bookReponsitory.GetBookById(id);
            return Ok(book);
        }

        //ADD BOOK METHOD
        [HttpPost("add-book")]
        public IActionResult AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            var bookAdd = _bookReponsitory.AddBook(addBookRequestDTO);
            return Ok(bookAdd);
        }

        //UPDATE BOOK METHOD
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var updateBook = _bookReponsitory.UpdateBookById(id, bookDTO);
            return Ok(updateBook);
        }

        //DELETE METHOD
        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deletedBook = _bookReponsitory.DeleteBookById(id);
            return Ok(deletedBook);
        }

    }
}

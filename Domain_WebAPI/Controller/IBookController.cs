using Catel.Pooling;
using Domain_WebAPI.Data;
using Domain_WebAPI.Filter;
using Domain_WebAPI.Model.DTO;
using Domain_WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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
        public IActionResult GetAll(string? filterOn, string? filterQuery,  string sortBy,  bool isAcending, int pageNumber = 1, int pageSize = 100)
        {
            var allBooks = _bookReponsitory.GetAllBooks(filterOn, filterQuery, sortBy, isAcending, pageNumber ,  pageSize);
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
        [ValidateModel]
        public IActionResult AddBook([FromBody]AddBookRequestDTO addBookRequestDTO)
        {
            //validate request
            if (!ValidateAddBook(addBookRequestDTO))
            {
                return BadRequest(ModelState);
            }
         
            
            if (ModelState.IsValid)
            {
                var bookAdd = _bookReponsitory.AddBook(addBookRequestDTO);
                return Ok(bookAdd);
            }
            else return BadRequest(ModelState);
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


        #region Private methods
        private bool ValidateAddBook(AddBookRequestDTO addBookRequestDTO)
        {
            if (addBookRequestDTO == null)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO), $"Please add book data");

                return false;
            }
            //check description not null
            if(string.IsNullOrEmpty(addBookRequestDTO.Description))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Description), $"{nameof(addBookRequestDTO.Description)} cannot be bull");
            }

            //check rating (0,5)

            if(addBookRequestDTO.Rate < 0 || addBookRequestDTO.Rate > 5)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.Rate), $"{nameof(addBookRequestDTO.Rate)} cannot be less than 0 and more than 5");

            }

            if (ModelState.ErrorCount > 0) { 
                return false;
            }

            return true;
        }
        #endregion
    }
}

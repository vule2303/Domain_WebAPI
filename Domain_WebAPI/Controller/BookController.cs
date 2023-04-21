using Domain_WebAPI.Data;
using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Domain_WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        
        public BookController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //GET httpL//localhost:port/api/get-all-book
        [HttpGet("get-all-books")]
        public IActionResult GetAll()
        {
            Debug.WriteLine("Dang chay GetAll");
            try
            {
                //var allBooksDomain = _dbContext.Books.ToList();
                //Get Data From Database -Domain Model
                var allBooksDomain = _dbContext.Books;
                //Map domain model vs DTO
                var allBooksDTO = allBooksDomain.Select(Books => new BookDTO()
                {
                    Id = Books.Id,
                    Title = Books.Title,
                    Description = Books.Description,
                    IsRead = Books.IsRead,
                    DateRead = Books.DateRead,
                    Rate = Books.Rate,
                    Genre = Books.Genre,
                    CoverUrl = Books.CoverUrl,
                    PublisherName = Books.Publisher.Name,
                    AuthorNames = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList();
                //return DTOs
                return Ok(allBooksDTO);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookId([FromRoute] int id)
        {
            //get book Domain model from Db
            var bookWitDomain = _dbContext.Books.Where(n => n.Id == id);
            if (bookWitDomain == null)
            {
                return NotFound();
            }
            //Map Domain model to DTOs
            var bookWithIdDTO = bookWitDomain.Select(book => new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            });
            return Ok(bookWithIdDTO);

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            //map DTO to Domain Model
            var bookDomainModel = new Book
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                IsRead = addBookRequestDTO.IsReal,
                DateRead = addBookRequestDTO.DateRead,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = addBookRequestDTO.DateAdded,
                PublisherID = addBookRequestDTO.PublisherID
            };
            //Use Domain Model to create Book
            _dbContext.Books.Add(bookDomainModel);
            _dbContext.SaveChanges();

            foreach (var id in addBookRequestDTO.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = bookDomainModel.Id,
                    AuthorId = id
                };
                _dbContext.Books_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }
            return Ok();

        }
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDomain != null)
            {
                bookDomain.Title = bookDTO.Title;
                bookDomain.Description = bookDTO.Description;
                bookDomain.IsRead = bookDTO.IsReal;
                bookDomain.DateRead = bookDTO.DateRead;
                bookDomain.Rate = bookDTO.Rate;
                bookDomain.Genre = bookDTO.Genre;
                bookDomain.CoverUrl = bookDTO.CoverUrl;
                bookDomain.DateAdded = bookDTO.DateAdded;
                bookDomain.PublisherID = bookDTO.PublisherID;
                _dbContext.SaveChanges();
            }
            var authorDomain = _dbContext.Books_Authors.Where(a => a.BookId == id).ToList();
            if (authorDomain != null)
            {
                _dbContext.Books_Authors.RemoveRange(authorDomain);
                _dbContext.SaveChanges();
            }
            foreach (var authorid in bookDTO.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = id,
                    AuthorId = authorid
                };
                _dbContext.Books_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }
            return Ok(bookDTO);
        }
        //actionDeleteById 
        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookbyID(int id)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDomain != null)
            {
                _dbContext.Books.Remove(bookDomain);
                _dbContext.SaveChanges();
            }
            return Ok();
        }
        
    }
}

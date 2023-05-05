using Domain_WebAPI.Data;
using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;

namespace Domain_WebAPI.Repositories
{
    public class SQLBookReponsitory : IBookReponsitory
    {
        private readonly AppDbContext _dbContext;
        public SQLBookReponsitory(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            //map Domain model to DTO
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
            //use Domain Model to add Book
            _dbContext.Books.Add(bookDomainModel);
            _dbContext.SaveChanges();

            //Add id into Book_Author table

            foreach(var id in addBookRequestDTO.AuthorIds)
            {
                Debug.WriteLine(id);
                var book_author = new Book_Author()
                {
                    BookId = bookDomainModel.Id,
                    AuthorId = id
                };
                _dbContext.Add(book_author);
                _dbContext.SaveChanges();
                
            }
            return addBookRequestDTO;
        }

        public Book? DeleteBookById(int id)
        {
            var bookDelete = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if (bookDelete != null)
            {
                _dbContext.Books.Remove(bookDelete);
                _dbContext.SaveChanges();
            }
            return bookDelete;
        }

        public List<BookDTO> GetAllBooks()
        {
            var allBooks = _dbContext.Books.Select(Books => new BookDTO()
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
            return allBooks;
        }

        public BookDTO GetBookById(int id)
        {
            var bookWithDomain = _dbContext.Books.Where(n => n.Id == id);
            //Map domain model to DTO
            var bookWithIdDTO = bookWithDomain.Select(Book => new BookDTO()
            {
                Id = Book.Id,
                Title = Book.Title,
                Description = Book.Description,
                IsRead = Book.IsRead,
                DateRead = Book.DateRead,
                Rate = Book.Rate,
                Genre = Book.Genre,
                CoverUrl = Book.CoverUrl,
                PublisherName = Book.Publisher.Name,
                AuthorNames = Book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();
            return bookWithIdDTO;
        }


        public AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO)
        {
            var bookDomain = _dbContext.Books.FirstOrDefault(n => n.Id == id);
            if(bookDomain != null)
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
            //Update id in book_author
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
            return bookDTO;


        }

        
    }
}

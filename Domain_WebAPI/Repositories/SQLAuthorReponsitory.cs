using Domain_WebAPI.Data;
using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.ComponentModel;

namespace Domain_WebAPI.Repositories
{
    public class SQLAuthorReponsitory : IAuthorReponsitory
    {
        private readonly AppDbContext _dbContext;

        public SQLAuthorReponsitory(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            //Map DTO to Domain Model
            var authorDomain = new Author
            {
                FullName = addAuthorRequestDTO.Name,             
            };
            //Save to Context
            _dbContext.Authors.Add(authorDomain);
            _dbContext.SaveChanges();

            //Add idBook to author

            foreach (var id in addAuthorRequestDTO.BookIds)
            {
                var _book_author = new Book_Author()
                {
                    AuthorId = authorDomain.Id,
                    BookId = id
                };

                // save change

                _dbContext.Books_Authors.Add(_book_author);
                _dbContext.SaveChanges();
            }

            return addAuthorRequestDTO;

        }

        public Author? DeleteById(int id)
        {
            //get id in author
            var authorDelete = _dbContext.Authors.FirstOrDefault(a => a.Id == id);

            if (authorDelete != null)
            {
                _dbContext.Authors.Remove(authorDelete);
                _dbContext.SaveChanges();
            }

            return authorDelete;
        }

        public List<AuthorDTO> GetAllAuthor()
        {
            var allAuthors = _dbContext.Authors.Select(Authors => new AuthorDTO()
            {
                ID = Authors.Id,
                Name = Authors.FullName,
                BookNames = Authors.Book_Authors.Select(n => n.Book.Title).ToList()
            }).ToList();
            return allAuthors;
        }

        public AuthorDTO GetAuthorById(int id)
        {
            //Get id author in domain model
            var AuthorDomain = _dbContext.Authors.Where(n => n.Id == id);
            //map domain model to DTO
            var IdAuthorDTO = AuthorDomain.Select(n => new AuthorDTO()
            {
                ID = n.Id,
                Name = n.FullName,
                BookNames = n.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return IdAuthorDTO;
        }

        public AddAuthorRequestDTO? UpdateAuthorById(int id, AddAuthorRequestDTO authorDTO)
        {
            //get id in Authors
            var AuthorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            
            if (AuthorDomain == null)
            {
                AuthorDomain.FullName = authorDTO.Name;
                
                _dbContext.SaveChanges();
            }

            //update list
            //var bookDomain = _dbContext.Books_Authors.Where(n => n.AuthorId == id).ToList();

            //if(bookDomain != null)
            //{
            //    //Nếu danh sách này không rỗng, chúng ta sẽ xóa tất cả các bản ghi này khỏi cơ sở dữ liệu.
            //    _dbContext.Books_Authors.RemoveRange(bookDomain);
            //    _dbContext.SaveChanges();

            //}

            foreach (var bookid in authorDTO.BookIds)
            {
                var _book_author = new Book_Author()
                {
                    AuthorId = id,
                    BookId = bookid
                };
                _dbContext.Books_Authors.Add(_book_author);
                _dbContext.SaveChanges();
             }
            return authorDTO;
        }
    }
}

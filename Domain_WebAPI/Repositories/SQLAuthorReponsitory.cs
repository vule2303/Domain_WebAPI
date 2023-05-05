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
            var authorDomainModel = new Author
            {
                FullName = addAuthorRequestDTO.FullName
            };
            //use Domain Model to Create Author
            _dbContext.Authors.Add(authorDomainModel);
            _dbContext.SaveChanges();
            return addAuthorRequestDTO;


        }

        public Author? DeleteById(int id)
        {
            //get id in author
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if(authorDomain != null)
            {
                _dbContext.Authors.Remove(authorDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }

        public List<AuthorDTO> GetAllAuthor()
        {
            //Get Data From Database -Domain Model
            var allAuthorsDomain = _dbContext.Authors.ToList();
            //Map Domain models to DTOS
            var allAuthorDTO = new List<AuthorDTO>();
            foreach (var authorDomain in allAuthorsDomain)
            {
                allAuthorDTO.Add(new AuthorDTO()
                {
                    Id = authorDomain.Id,
                    FullName = authorDomain.FullName
                }) ;
            }
            return allAuthorDTO;
        }

        public authorNoIdDTO GetAuthorById(int id)
        {
            //Get id author in domain model
            var authorWithIdDomain = _dbContext.Authors.FirstOrDefault(x => x.Id == id);
            if (authorWithIdDomain == null)
            {
                return null;
            }

            //map domain model to DTO
            var authorNoIdDTO = new authorNoIdDTO
            {
                FullName = authorWithIdDomain.FullName
            };
            return authorNoIdDTO;
        }

        public authorNoIdDTO UpdateAuthorById(int id, authorNoIdDTO authorDTO)
        {
            var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            if(authorDomain != null)
            {
                authorDomain.FullName = authorDTO.FullName;
                _dbContext.SaveChanges();
            }
                        
            return authorDTO;
        }
    }
}

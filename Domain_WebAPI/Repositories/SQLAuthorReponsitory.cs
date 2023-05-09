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

        public List<AuthorDTO> GetAllAuthor(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAcending = true, int pageNumber = 1, int pageSize = 1000)
        {

            //Get Data From Database -Domain Model
            var allAuthorsDomain = _dbContext.Authors.Select(Author => new AuthorDTO()
            {
                Id = Author.Id,
                FullName = Author.FullName
            }).AsQueryable();

            //Map Domain models to DTOS
            //var allAuthorDTO = new List<AuthorDTO>();
            //foreach (var authorDomain in allAuthorsDomain)
            //{
            //    allAuthorDTO.Add(new AuthorDTO()
            //    {
            //        Id = authorDomain.Id,
            //        FullName = authorDomain.FullName
            //    });
            //}
            //allAuthorsDomain.AsQueryable();


            //filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("FullName", StringComparison.OrdinalIgnoreCase))
                {
                    allAuthorsDomain = allAuthorsDomain.Where(x => x.FullName.Contains(filterQuery));
                }
            }
            //sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("FullName", StringComparison.OrdinalIgnoreCase))
                {
                    allAuthorsDomain = isAcending?allAuthorsDomain.OrderBy(x => x.FullName):allAuthorsDomain.OrderByDescending(x => x.FullName);
                }
            }
            //paganation
            var skipResults = (pageNumber - 1) * pageSize;
            return allAuthorsDomain.Skip(skipResults).Take(pageSize).ToList();
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

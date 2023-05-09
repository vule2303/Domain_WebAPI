using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;

namespace Domain_WebAPI.Repositories
{
    public interface IAuthorReponsitory
    {
        List<AuthorDTO> GetAllAuthor(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAcending = true, int pageNumber = 1, int pageSize = 1000);
        authorNoIdDTO GetAuthorById (int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
        authorNoIdDTO UpdateAuthorById(int id, authorNoIdDTO authorDTO);
        Author? DeleteById(int id);
    }
}

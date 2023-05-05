using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;

namespace Domain_WebAPI.Repositories
{
    public interface IAuthorReponsitory
    {
        List<AuthorDTO> GetAllAuthor();
        authorNoIdDTO GetAuthorById (int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
        authorNoIdDTO UpdateAuthorById(int id, authorNoIdDTO authorDTO);
        Author? DeleteById(int id);
    }
}

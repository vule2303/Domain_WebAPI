using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;

namespace Domain_WebAPI.Repositories
{
    public interface IAuthorReponsitory
    {
        List<AuthorDTO> GetAllAuthor();
        BookDTO GetAuthorById (int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
        AddAuthorRequestDTO? UpdateAuthorById(int id, AddAuthorRequestDTO authorDTO);
        Author? DeleteById(int id);
    }
}

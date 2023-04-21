using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;

namespace Domain_WebAPI.Repositories
{
    public interface IBookReponsitory
    {
        List<BookDTO> GetAllBooks();
        BookDTO GetBookById(int id);
        AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
        AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
        Book? DeleteBookById(int id);
    }
}

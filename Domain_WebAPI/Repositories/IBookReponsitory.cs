using Domain_WebAPI.Model.Domain;
using Domain_WebAPI.Model.DTO;
using System.Runtime.CompilerServices;

namespace Domain_WebAPI.Repositories
{
    public interface IBookReponsitory
    {
        List<BookDTO> GetAllBooks(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
        BookDTO GetBookById(int id);
        AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
        AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
        Book? DeleteBookById(int id);
    }
}

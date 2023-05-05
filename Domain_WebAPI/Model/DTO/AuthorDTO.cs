using System.Runtime.CompilerServices;

namespace Domain_WebAPI.Model.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class authorNoIdDTO
    {
        public string FullName { get; set; }
    }
}

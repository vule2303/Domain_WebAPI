using System.Runtime.CompilerServices;

namespace Domain_WebAPI.Model.DTO
{
    public class AuthorDTO
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public List<string> BookNames { get; set; }
    }
}

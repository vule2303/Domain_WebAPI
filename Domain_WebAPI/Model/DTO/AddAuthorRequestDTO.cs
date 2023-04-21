namespace Domain_WebAPI.Model.DTO
{
    public class AddAuthorRequestDTO
    {
        public string Name { get; set; }
        public List<int> BookIds { get; set; }
    }
}

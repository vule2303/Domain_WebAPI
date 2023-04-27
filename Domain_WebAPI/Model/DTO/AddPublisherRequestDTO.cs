namespace Domain_WebAPI.Model.DTO
{
    public class AddPublisherRequestDTO
    {
        public string Name { get; set; }

        public List<int>? BookIds { get; set; }
    }
}

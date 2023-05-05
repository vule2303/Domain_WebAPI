namespace Domain_WebAPI.Model.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PublisherNoIdDTO
    {
        public string Name { get; set;}
    }

    public class PublisherWithBooksAndAuthorsDTO
    {
        public string Name { get; set; }
        public List<BookAuthorDTO> BookAuthors { get; set; }
    }
}

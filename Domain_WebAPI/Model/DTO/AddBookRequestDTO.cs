using System.ComponentModel.DataAnnotations;

namespace Domain_WebAPI.Model.DTO
{
    public class AddBookRequestDTO
    {
        [Required]
        [MinLength(10)] //khi người dùng thêm book thì trường title là bắt buộc và tối thiếu 10 ký tự
        public string? Title { get; set; }
        public string Description { get; set; }
        public bool IsReal { get; set; }
        public DateTime? DateRead { get; set; }
        [Range(0, 5, ErrorMessage = "From 0 to 5")]
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        //navigation Properties-
        public int PublisherID { get; set; }
        public List<int>? AuthorIds { get;set; }
    }
} 

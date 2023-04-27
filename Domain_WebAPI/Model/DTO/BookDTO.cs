﻿namespace Domain_WebAPI.Model.DTO
{
    //
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublisherName { get; set; }
        public List<string>? AuthorNames { get; set; }
    }
}

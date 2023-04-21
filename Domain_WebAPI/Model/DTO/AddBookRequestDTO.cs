﻿namespace Domain_WebAPI.Model.DTO
{
    public class AddBookRequestDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsReal { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        //navigation Properties-
        public int PublisherID { get; set; }
        public List<int> AuthorIds { get;set; }
    }
} 

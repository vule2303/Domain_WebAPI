﻿using Domain_WebAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace Domain_WebAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) :
base(dbContextOptions)
        {
            //constructor
        }
        //define C# model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { // có thể dinh nghia moi quan he giua cac table bang Fluent API
            modelBuilder.Entity<Book_Author>()
            .HasOne(b => b.Book)
            .WithMany(ba => ba.Book_Authors)
            .HasForeignKey(bi => bi.BookId);
            modelBuilder.Entity<Book_Author>()
            .HasOne(b => b.Author)
            .WithMany(ba => ba.Book_Authors)
            .HasForeignKey(bi => bi.AuthorId);
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}

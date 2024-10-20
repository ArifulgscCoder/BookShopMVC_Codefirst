using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookShopMVCmidExam_1280425.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base("AppDBContext")
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreEntry> GenreEntries { get; set; }
    }
}
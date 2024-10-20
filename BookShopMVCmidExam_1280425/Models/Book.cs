using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShopMVCmidExam_1280425.Models
{
    public partial class Book
    {
        public Book()
        {
            this.GenreEntries = new List<GenreEntry>();
        }
        [Key]
        public int BookId { get; set; }
        [Required, StringLength(30, ErrorMessage = "Book Name cannot be longer than 30 characters."), Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Required, StringLength(30, ErrorMessage = "Author Name cannot be longer than 30 characters."), Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        [Required, Display(Name = "Publish Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PublishDate { get; set; }
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$")]
        public string EmailAddress { get; set; }
        public string Picture { get; set; }
        public bool InStock { get; set; }
        [Range(100, 500000)]
        public int Price { get; set; }
        public ICollection<GenreEntry> GenreEntries { get; set; }
    }
}
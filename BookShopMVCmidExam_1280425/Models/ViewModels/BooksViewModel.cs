using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShopMVCmidExam_1280425.Models.ViewModels
{
    public class BooksViewModel
    {
        public BooksViewModel()
        {
            this.GenresList = new List<int>();
        }
        [Key]
        public int BookId { get; set; }
        [Required, StringLength(30, ErrorMessage = "Book Name cannot be longer than 30 characters."), Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Required, StringLength(30, ErrorMessage = "Author Name cannot be longer than 30 characters."), Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        [Required, Display(Name = "Publish Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [Display(Name = "Author Email")]
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$")]
        public string EmailAddress { get; set; }
        public string Picture { get; set; }
        public bool InStock { get; set; }
        [Range(100, 5000000)]
        public int Price { get; set; }
        public HttpPostedFileBase PicturePath { get; set; }
        public List<int> GenresList { get; set; }
    }
}
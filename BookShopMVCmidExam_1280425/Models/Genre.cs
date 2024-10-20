using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShopMVCmidExam_1280425.Models
{
    public partial class Genre
    {
        public Genre()
        {
            this.GenreEntries = new List<GenreEntry>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int GenreId { get; set; }
        [Required, StringLength(50), Display(Name = "Genre Name")]
        public string GenreTitle { get; set; }
        public virtual ICollection<GenreEntry> GenreEntries { get; set; }
    }
}
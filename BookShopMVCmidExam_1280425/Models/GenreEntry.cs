using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShopMVCmidExam_1280425.Models
{
    public partial class GenreEntry
    {
        [Key]
        public int GenreEntriesId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
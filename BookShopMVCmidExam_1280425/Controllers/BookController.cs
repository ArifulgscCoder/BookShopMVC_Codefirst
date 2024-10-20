using BookShopMVCmidExam_1280425.Models.ViewModels;
using BookShopMVCmidExam_1280425.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BookShopMVCmidExam_1280425.Controllers
{
    public class BookController : Controller
    {
        private AppDBContext db = new AppDBContext();
        public ActionResult Index()
        {
            var books = db.Books.Include(c => c.GenreEntries.Select(b => b.Genre)).OrderByDescending(x => x.BookId).ToList();
            return View(books);
        }
        public ActionResult AddNewGenres(int? id)
        {
            ViewBag.sports = new SelectList(db.Genres.ToList(), "GenreId", "GenreTitle", (id != null) ? id.ToString() : "");
            return PartialView("_AddNewGenres");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BooksViewModel booksViewModel, int[] GenreId)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book()
                {
                    BookName = booksViewModel.BookName,
                    AuthorName = booksViewModel.AuthorName,
                    PublishDate = booksViewModel.PublishDate,
                    EmailAddress = booksViewModel.EmailAddress,
                    Price = booksViewModel.Price,
                    InStock = booksViewModel.InStock
                };
                HttpPostedFileBase file = booksViewModel.PicturePath;
                if (file != null)
                {
                    string fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine("/Images", fileName);
                    file.SaveAs(Server.MapPath(filePath));
                    book.Picture = filePath;
                }
                foreach (var item in GenreId)
                {
                    GenreEntry genreEntry = new GenreEntry()
                    {
                        Book = book,
                        BookId = book.BookId,
                        GenreId = item
                    };
                    db.GenreEntries.Add(genreEntry);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            Book book = db.Books.First(x => x.BookId == id);
            var bookGenre = db.GenreEntries.Where(x => x.BookId == id).ToList();
            BooksViewModel booksViewModel = new BooksViewModel()
            {
                BookId = book.BookId,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                PublishDate = book.PublishDate,
                EmailAddress = book.EmailAddress,
                Price = book.Price,
                Picture = book.Picture,
                InStock = book.InStock
            };
            if (bookGenre.Count() > 0)
            {
                foreach (var item in bookGenre)
                {
                    booksViewModel.GenresList.Add(item.GenreId);
                }
            }
            return View(booksViewModel);
        }
        [HttpPost]
        public ActionResult Edit(BooksViewModel booksViewModel, int[] GenreId)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book()
                {
                    BookId = booksViewModel.BookId,
                    BookName = booksViewModel.BookName,
                    AuthorName = booksViewModel.AuthorName,
                    PublishDate = booksViewModel.PublishDate,
                    EmailAddress = booksViewModel.EmailAddress,
                    Price = booksViewModel.Price,
                    Picture = booksViewModel.Picture,
                    InStock = booksViewModel.InStock
                };
                HttpPostedFileBase file = booksViewModel.PicturePath;
                if (file != null)
                {
                    string fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine("/Images", fileName);
                    file.SaveAs(Server.MapPath(filePath));
                    book.Picture = filePath;
                }
                else
                {
                    book.Picture = booksViewModel.Picture;
                }
                var existsGenreEntry = db.GenreEntries.Where(x => x.BookId == book.BookId).ToList();
                foreach (var genreEntry in existsGenreEntry)
                {
                    db.GenreEntries.Remove(genreEntry);
                }
                foreach (var item in GenreId)
                {
                    GenreEntry genreEntry = new GenreEntry()
                    {
                        Book = book,
                        BookId = book.BookId,
                        GenreId = item
                    };
                    db.GenreEntries.Add(genreEntry);
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            var book = db.Books.Find(id);
            var existsGenresEntry = db.GenreEntries.Where(x => x.BookId == book.BookId).ToList();
            foreach (var genreEntry in existsGenresEntry)
            {
                db.GenreEntries.Remove(genreEntry);
            }
            db.Entry(book).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BookList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var book = from p in db.Books select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                book = book.Where(p => p.BookName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    book = book.OrderByDescending(p => p.BookName);
                    break;
                case "Date":
                    book = book.OrderBy(p => p.PublishDate);
                    break;
                case "date_desc":
                    book = book.OrderByDescending(p => p.PublishDate);
                    break;
                default:
                    book = book.OrderBy(p => p.BookName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(book.ToPagedList(pageNumber, pageSize));
        }
    }
}
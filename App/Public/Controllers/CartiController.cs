
namespace Public.Controllers
{
    using BL.Managers;
    using BL.Helpers;
    using DAL.Entities;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;
    using Public.Models;
    using System;

    public class CartiController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var books = BooksManager.GetAllBooksWithDomain().Where(b => b.BookDomain.CZU != "4");

            var bookGroups = books.GroupBy(b => b.BookDomain.CZU);

            ViewData["bookGroups"] = bookGroups;

            return View();
        }

        public ActionResult Inexistentă()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult SimpleSearch(string simpleSearchText)
        {
            var books = new List<Book>();
            var authors = new List<Author>();
            var domains = new List<BookDomain>();

            if (!string.IsNullOrEmpty(simpleSearchText) && simpleSearchText.Length > 1)
            {
                books = SearchManager.GetBooksBySimpleSearch(simpleSearchText);
                authors = SearchManager.GetAuthorsBySimpleSearch(simpleSearchText);
                domains = SearchManager.GetBookDomainsBySimpleSearch(simpleSearchText);
            }

            ViewData["searchTerm"] = simpleSearchText;
            ViewData["domains"] = domains;
            ViewData["authors"] = authors;

            return View("Cauta", books);
        }

        [HttpGet]
        public ActionResult MultifieldSearch(string multiFieldTitle, string multiFieldAuthors, string multiFieldPublishers, string multiFieldDomains)
        {
            var books = new List<Book>();
            var authors = new List<Author>();
            var domains = new List<BookDomain>();

            books = SearchManager.GetBooksByMultiFieldSearch(multiFieldTitle, multiFieldPublishers.ToNullableInt(), multiFieldDomains.ToNullableInt(), multiFieldAuthors.ToNullableInt());
            foreach (var item in books)
            {
                item.Authors = BooksManager.GetBookAuthorsByBookId(item.Id) as List<Author>;
            }
            ViewData["books"] = books;
            ViewData["domains"] = domains;
            ViewData["authors"] = authors;

            return View("Cauta", books);
        }

        public ActionResult Detalii(int? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("Inexistentă");
            }

            var book = BooksManager.GetBookById(id.Value);
            if (book == null)
            {
                return RedirectToAction("Inexistentă");
            }
            else
            {
                book.BookStatus = LoansManager.GetBookLoanStatus(book.Id);
                if (book.BookStatus == LoanReservedBookStatus.Disponibilă)
                {
                    book.ReservedUntil = ReservationsManager.IsReservedGetDate(id.Value);
                    if(book.ReservedUntil != null)
                    {
                        book.BookStatus = LoanReservedBookStatus.Rezervată;
                    }
                }
            }

            return View(book);
        }

        [HttpGet]
        public ActionResult RezervaPartial(int bookId)
        {
            var book = BooksManager.GetBookById(bookId);

            return PartialView("_BookABook", book);
        }

        [HttpPost]
        public ActionResult Rezerva(int bookId)
        {
            var userId = Session["userId"].ToString().ToNullableInt();

            if (!userId.HasValue || bookId <= 0)
            {
                return View("Index");
            }

            ReservationsManager.Add(
                new Reservation
                {
                    User = new User
                    {
                        Id = userId.Value
                    },
                    ReservedDate = DateTime.Now,
                    Book = new Book
                    {
                        Id = bookId
                    }
                });

            return RedirectToAction("Index");
        }

    }
}
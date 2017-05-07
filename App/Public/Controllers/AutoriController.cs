
namespace Public.Controllers
{
    using BL.Managers;
    using DAL.Entities;
    using Public.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class AutoriController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int? page)
        {
            int pageSize = 15;
            var authors = AuthorsManager.GetAllAuthors();
            var authorsCount = authors.Count();

            var pager = new Pager(authorsCount, page, pageSize);
            var viewModel = new IndexViewModel
            {
                Items = authors.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };

            return View(viewModel);
        }

        public ActionResult Detalii(int id, string name = null)
        {
            var author = AuthorsManager.GetById(id);
            if (author == null)
            {
                RedirectToAction("Index");
            }
            else if (name == null)
            {
                return this.RedirectToAction("Detalii", new { id = author.Id, name = author.Name.Replace(' ', '-') });
            }

            IEnumerable<Book> books = BooksManager.GetBooksByAuthorId(id, 1);

            ViewData["author"] = author;
            ViewData["searchTerm"] = author.Name;
            return View("../Carti/Cauta", books);
        }

        public ActionResult test()
        {
            return View();
        }
    }
}
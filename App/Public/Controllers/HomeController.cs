
namespace Public.Controllers
{
    using BL.Managers;
    using Public.Models;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var mainmodel = new MainPageModel
            {
                BookNumber = BooksManager.GetBooksNrAll(),
                LastAddedBooks = BooksManager.GetBooksLastAdded(20)
            };

            return View(mainmodel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}
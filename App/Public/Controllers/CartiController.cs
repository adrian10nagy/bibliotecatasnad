
namespace Public.Controllers
{
    using BL.Managers;
    using System.Web.Mvc;

    public class CartiController : Controller
    {

        [HttpGet]
        public ActionResult SimpleSearch(string simpleSearchText)
        {
            var books = SearchManager.GetBooksBySimpleSearch(simpleSearchText);

            ViewData["searchTerm"] = simpleSearchText;
            ViewData["books"] = books;
            return View("Cauta", books);
        }
    }
}
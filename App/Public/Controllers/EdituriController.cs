
namespace Public.Controllers
{
    using BL.Managers;
    using System.Web.Mvc;

    public class EdituriController : BaseController
    {

        public ActionResult Index()
        {
            var publishers = PublishersManager.GetAllPublishers();

            return View(publishers);
        }

        public ActionResult Detalii(int id)
        {
            var books = BooksManager.GetBooksByPublisherId(id);

            var publisher = PublishersManager.GetPublishersById(id);

            if (publisher != null)
            {
                ViewData["publisherName"] = publisher.Name;
            }

            return View(books);
        }
    }
}
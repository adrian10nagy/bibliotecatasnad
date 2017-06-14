
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

        public ActionResult Detalii(int id, string name = null)
        {
            var publisher = PublishersManager.GetPublishersById(id);

            if(publisher == null)
            {
                return RedirectToAction("Inexistentă");
            }
            else if(name == null)
            {
                return this.RedirectToAction("Detalii", new { id = publisher.Id, name = publisher.Name.Replace(' ', '-') });
            }

            var books = BooksManager.GetBooksByPublisherId(id, 1);

            if (publisher != null)
            {
                ViewData["publisherName"] = publisher.Name;
            }

            return View(books);
        }
    }
}

namespace Public.Controllers
{
    using BL.Managers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class DomeniiController : BaseController
    {
        // GET: Domenii
        public ActionResult Detalii(int id)
        {
            var domain = BookDomainsManager.GetBookDomainById(id);
            if(domain == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var books = BooksManager.GetAllBooksByDomainId(id);

            ViewData["searchTerm"] = domain.Name;

            return View("../Carti/Cauta", books);
        }
    }
}
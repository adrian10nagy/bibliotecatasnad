
namespace Public.Controllers
{
    using BL.Managers;
    using Public.Models;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            bool newUserParam = Request.QueryString["newRegister"] != null && Request.QueryString["newRegister"].ToString() == "true"? true : false;
            var flags = PageQueryParam.None;
             
            if(newUserParam)
            {
                flags = PageQueryParam.NewUser;
            }

            var mainmodel = new MainPageModel
            {
                BookNumber = BooksManager.GetBooksNrAll(1), // todo find library Id
                LastAddedBooks = BooksManager.GetBooksLastAdded(20, 1), // todo find library Id
                Flags = flags
            };

            return View(mainmodel);
        }

        public ActionResult Despre()
        {
            var library = LibrariesManager.GetLibraryById(1);

            return View(library);
        }

        public ActionResult Contact()
        {
            var library = LibrariesManager.GetLibraryById(1);

            return View(library);
        }
    }
}
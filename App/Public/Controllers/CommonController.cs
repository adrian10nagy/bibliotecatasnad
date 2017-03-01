using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.Managers;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class CommonController : Controller
    {
        [HttpGet]
        public ActionResult AsyncGetSimpleSearchSuggestions(string searchTerm)
        {
            var bookSuggestions = SearchManager.GetBooksBySimpleSearch(searchTerm);

            ViewData["bookSuggestions"] = bookSuggestions;

            return PartialView("_SearchSuggestions");
        }
    }
}
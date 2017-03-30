using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.Managers;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class CommonController : BaseController
    {
        [HttpGet]
        public ActionResult AsyncGetSimpleSearchSuggestions(string searchTerm)
        {
            var bookSuggestions = SearchManager.GetBooksBySimpleSearch(searchTerm).Take(10).ToList();
            var authorsSuggestions = SearchManager.GetAuthorsBySimpleSearch(searchTerm).Take(5).ToList();
            var domainsSuggestions = SearchManager.GetBookDomainsBySimpleSearch(searchTerm).Take(5).ToList();

            ViewData["bookSuggestions"] = bookSuggestions;
            ViewData["authorsSuggestions"] = authorsSuggestions;
            ViewData["domainsSuggestions"] = domainsSuggestions;

            return PartialView("_SearchSuggestions");
        }
    }
}
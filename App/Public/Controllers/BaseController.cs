using BL.Managers;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // retireve data
            var authors = InitializeAuthors();
            var publishers = InitializePublishers();
            var domains = InitializeDomains();

            // set to viewbag
            ViewBag.MainSearchAuthors = authors;
            ViewBag.MainSearchPublishers = publishers;
            ViewBag.MainSearchDomains = domains;
        }

        private List<SelectListItem> InitializeAuthors()
        {
            var authors = AuthorsManager.GetAllAuthors();
            var listItemAuthors = new List<SelectListItem>();
            listItemAuthors.Add(new SelectListItem
            {
                Text="",
                Value = ""
            });

            listItemAuthors.AddRange(authors.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }));

            return listItemAuthors;
        }

        private List<SelectListItem> InitializePublishers()
        {
            var pulishers = PublishersManager.GetAllPublishers();
            var listItemPublishers = new List<SelectListItem>();
            listItemPublishers.Add(new SelectListItem
            {
                Text = "",
                Value = ""
            });

            listItemPublishers.AddRange(pulishers.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }));

            return listItemPublishers;
        }

        private List<SelectListItem> InitializeDomains()
        {
            var domains = BookDomainsManager.GetAllBookDomains();
            var listItemDomains = new List<SelectListItem>();
            listItemDomains.Add(new SelectListItem
            {
                Text = "",
                Value = ""
            });

            listItemDomains.AddRange(domains.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }));

            return listItemDomains;
        }
    }
}
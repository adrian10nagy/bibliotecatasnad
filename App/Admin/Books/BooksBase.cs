
namespace Admin.Books
{
    using BL.Constants;
    using BL.Managers;
    using DAL.Entities;
    using System;

    public class BooksBase : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;
            if (user == null || !UserRightsManager.CanAccessBooksModule(user.Id))
            {
                Response.Redirect("~/");
            }
        }
    }
}
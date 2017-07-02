
namespace Admin.Users
{
    using BL.Constants;
    using BL.Managers;
    using DAL.Entities;
    using System;

    public class UsersBase : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;
            if (user == null || !UserRightsManager.CanAccesUsersModule(user.Id))
            {
                Response.Redirect("~/");
            }
        }
    }
}
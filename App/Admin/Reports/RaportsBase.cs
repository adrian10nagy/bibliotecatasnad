
namespace Admin.Reports
{
    using BL.Constants;
    using BL.Managers;
    using DAL.Entities;
    using System;

    public class RaportsBase : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;
            if (user == null || !UserRightsManager.CanAccessRaportsModule(user.Id))
            {
                Response.Redirect("~/");
            }
        }
    }
}
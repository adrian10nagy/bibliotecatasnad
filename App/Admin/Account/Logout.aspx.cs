
namespace Admin.Account
{
    using BL.Constants;
    using System;

    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove(SessionConstants.LoginUser);
            Response.Redirect("~/Account/Login.aspx?Message=Logout");
        }
    }
}
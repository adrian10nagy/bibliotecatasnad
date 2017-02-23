
namespace Admin.Account
{
    using Admin.Helpers;
    using BL.Constants;
    using System;

    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionHelper.Instance.RemoveSessionItem(CacheConstants.LoginUser);
            Response.Redirect("~/Account/Login.aspx?Message=Logout");
        }
    }
}
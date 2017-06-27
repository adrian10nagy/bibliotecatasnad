
namespace Admin
{
    using BL.Cache;
    using BL.Constants;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;
            if (user == null || user.Id == 0)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                txtWelcomeUserName.Text = user.LastName + " " + user.FirstName;
                txtMenuTopUserName.Text = user.LastName + " " + user.FirstName;
                txtWelcomeLibrary.Text = "Biblioteca " + user.Library.Name;
                txtWelcomeLibraryFooter.Text = "Biblioteca " + user.Library.Name;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;

            LoadMainLogoImage(user);
        }

        private void LoadMainLogoImage(User user)
        {
            var imageUrl = "/Content/Images/logo.png";

            var imageFormatUrl = string.Format("~/Content/Images/logo{0}.png", user.Library.Id);

            if (File.Exists(Server.MapPath(imageFormatUrl)))
            {
                imageUrl = imageFormatUrl;
            }

            MainLogo.ImageUrl = imageUrl;
        }
    }
}
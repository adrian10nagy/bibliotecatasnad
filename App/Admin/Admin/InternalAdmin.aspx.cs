using BL.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Entities;
using BL.Managers;

namespace Admin.Reports
{
    public partial class InternalAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = Session[SessionConstants.LoginUser] as User;
            if(user.UserType != UserType.Administrator 
                || user.Id != 1)
            {
                Response.Redirect("~/");
            }
        }

        protected void btnLibraryAdd_Click(object sender, EventArgs e)
        {
            pnlAddPartner.Visible = true;
        }

        protected void btnPartnerAddSubmit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPartenerAddName.Text))
            {
                txtPartenerAddName.CssClass += "";
            }

            LibrariesManager.Add(new Library
            {
                Name = txtPartenerAddName.Text
            });
            pnlAddPartner.Visible = false;
        }
    }
}
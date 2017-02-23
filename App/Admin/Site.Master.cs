﻿
namespace Admin
{
    using Admin.Helpers;
    using BL.Cache;
    using BL.Constants;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var user = SessionHelper.Instance.GetMySessionItem(CacheConstants.LoginUser) as User;
            if(user == null || user.Id == 0)
            {
                Response.Redirect("~/Account/Login.aspx?Message=SessionTimeout");
            }
            else
            {
                txtWelcomeUserName.Text = user.FirstName + " " + user.LastName;
                txtMenuTopUserName.Text = user.FirstName + " " + user.LastName;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}

namespace Admin
{
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Web.UI;
    

    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var x = UsersManager.GetUserById(1);
            var y = x;
        }
    }
}
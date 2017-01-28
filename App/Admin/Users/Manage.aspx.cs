
namespace Admin.Users
{
    using BL.Managers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DAL.Entities;

    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitializeUsersTable();
            }
        }

        private void InitializeUsersTable()
        {
            var users = UsersManager.GetUsersAll();

            foreach (User user in users)
            {
                TableRow row = new TableRow();
                HyperLink link = new HyperLink
                {
                    NavigateUrl = "~/User/Details.aspx?userId" + user.Id,
                    CssClass = "toClickOn",
                    Text = "Vezi detalii"
                };
                TableCell userId = new TableCell();
                userId.Controls.Add(link);
                row.Cells.Add(userId);

                TableCell userFirstName = new TableCell
                {
                    Text = user.FirstName
                };
                row.Cells.Add(userFirstName);

                TableCell userLastName = new TableCell
                {
                    Text = user.LastName
                };
                row.Cells.Add(userLastName);

                TableCell userusername = new TableCell
                {
                    Text = user.Username
                };
                row.Cells.Add(userusername);

                TableCell userHomeAddress = new TableCell
                {
                    Text = user.HomeAddress
                };
                row.Cells.Add(userHomeAddress);

                TableCell userBirthdate = new TableCell
                {
                    Text = user.Birthdate.ToShortDateString()
                };
                row.Cells.Add(userBirthdate);

                TableCell userPhone = new TableCell
                {
                    Text = user.Phone
                };
                row.Cells.Add(userPhone);

                TableCell userEmail = new TableCell
                {
                    Text = user.Email
                };
                row.Cells.Add(userEmail);

                TableCell userFacebook = new TableCell
                {
                    Text = user.FacebookAddress
                };
                row.Cells.Add(userFacebook);

                TableCell userGender = new TableCell
                {
                    Text = user.Gender.ToString()
                };
                row.Cells.Add(userGender);

                TableCell userLocality = new TableCell
                {
                    Text = user.Locality.Name.ToString()
                };
                row.Cells.Add(userLocality);

                datatableResponsive.Rows.Add(row);
            }
        }
    }
}
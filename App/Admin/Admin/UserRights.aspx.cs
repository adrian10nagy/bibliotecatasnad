using BL.Constants;
using BL.Managers;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Helpers;

namespace Admin.Admin
{
    public partial class UserRights : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeUsersTable();
        }


        private void InitializeUsersTable()
        {
            var sessionUser = Session[SessionConstants.LoginUser] as User;
            var users = UsersManager.GetUsersAll(sessionUser.Library.Id);
            var userRights = UserRightsManager.UsersWithRights();
            var userNameFormat = "{0} {1}";
            var i = 1;

            foreach (User user in users)
            {
                TableRow row = new TableRow();

                TableCell userName = new TableCell
                {
                    Text = string.Format(userNameFormat, user.FirstName, user.LastName)
                };
                row.Cells.Add(userName);

                TableCell username = new TableCell
                {
                    Text = user.Username
                };
                row.Cells.Add(username);

                TableCell adminLoginRightCell = new TableCell();
                CheckBox chbAdminLoginRightCell = new CheckBox();
                chbAdminLoginRightCell.AutoPostBack = true;
                chbAdminLoginRightCell.ID = String.Format("adminLoginRights{0}", i);
                chbAdminLoginRightCell.Checked = xhbChecked(userRights, user.Id, Functionality.AdminLogin);
                chbAdminLoginRightCell.CheckedChanged += chb_CheckedChanged;
                adminLoginRightCell.Controls.Add(chbAdminLoginRightCell);
                row.Cells.Add(adminLoginRightCell);

                TableCell bookAddRightCell = new TableCell();
                CheckBox chbBookRightCell = new CheckBox();
                chbBookRightCell.AutoPostBack = true;
                chbBookRightCell.ID = String.Format("bookRights{0}", i);
                chbBookRightCell.CheckedChanged += chb_CheckedChanged;
                chbBookRightCell.Checked = xhbChecked(userRights, user.Id, Functionality.ManageBooks);
                bookAddRightCell.Controls.Add(chbBookRightCell);
                row.Cells.Add(bookAddRightCell);

                TableCell userRightCell = new TableCell();
                CheckBox chbUserRightCell = new CheckBox();
                chbUserRightCell.AutoPostBack = true;
                chbUserRightCell.ID = String.Format("userRights{0}", i);
                chbUserRightCell.CheckedChanged += chb_CheckedChanged;
                chbUserRightCell.Checked = xhbChecked(userRights, user.Id, Functionality.ManageUsers);
                userRightCell.Controls.Add(chbUserRightCell);
                row.Cells.Add(userRightCell);

                TableCell raportRightCell = new TableCell();
                CheckBox chbRaportRightCell = new CheckBox();
                chbRaportRightCell.AutoPostBack = true;
                chbRaportRightCell.ID = String.Format("raportRights{0}", i);
                chbRaportRightCell.Checked = xhbChecked(userRights, user.Id, Functionality.Raports);
                chbRaportRightCell.CheckedChanged += chb_CheckedChanged;
                raportRightCell.Controls.Add(chbRaportRightCell);
                row.Cells.Add(raportRightCell);

                datatableResponsive.Rows.Add(row);
                i++;
            }
        }

        private bool xhbChecked(IEnumerable<UserRight> userRights, int userID, Functionality functionality)
        {
            var result = false;
            if (userRights.Where(ur => ur.User.Id == userID && ur.Functionality == functionality).Any())
            {
                result = true;
            }

            return result;
        }

        void chb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            int? rowNr = chb.ID.Replace("raportRights", "").Replace("userRights", "")
                .Replace("bookRights", "").Replace("adminLoginRights", "").ToNullableInt();

            if (rowNr.HasValue)
            {
                TableRow row = datatableResponsive.Rows[rowNr.Value];
                if (row != null)
                {
                    TableCell usernameCell = row.Cells[1];
                    if (usernameCell != null && !string.IsNullOrEmpty(usernameCell.Text))
                    {
                        Functionality? functionality = MapCheckboxWithFunctionality(chb.ID);
                        if (functionality.HasValue)
                        {
                            var userRight = new UserRight
                            {
                                hasRight = chb.Checked,
                                User = new User
                                {
                                    Username = usernameCell.Text
                                },
                                Functionality = functionality.Value
                            };

                            UserRightsManager.UpdateRight(userRight);
                        }
                    }
                }
            }
        }

        private Functionality? MapCheckboxWithFunctionality(string p)
        {
            Functionality? result = null;

            if (p.StartsWith("raportRights"))
            {
                result = Functionality.Raports;
            }
            else if (p.StartsWith("userRights"))
            {
                result = Functionality.ManageUsers;
            }
            else if (p.StartsWith("bookRights"))
            {
                result = Functionality.ManageBooks;
            }
            else if (p.StartsWith("adminLoginRights"))
            {
                result = Functionality.AdminLogin;
            }
            else
            {
                result = null;
            }

            return result;
        }
    }
}
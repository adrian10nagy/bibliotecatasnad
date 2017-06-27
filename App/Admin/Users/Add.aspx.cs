
namespace Admin.Users
{
    using Helpers.Constants;
    using BL.Constants;
    using BL.Helpers;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;

    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.InitializeNationalities();
                this.InitializeLocalities();
                this.InitializeUserTypes();

                if (!string.IsNullOrEmpty(Request["userId"]))
                {
                    var userId = Request["userId"].ToNullableInt();
                    if (userId != null && userId != 0)
                    {
                        var user = UsersManager.GetUserById(userId.Value);
                        if (user != null)
                        {
                            userName.Value = user.FirstName;
                            userSurname.Value = user.LastName;
                            userAddress.Value = user.HomeAddress;
                            userBirthday.Value = user.Birthdate.ToString("dd-MM-yyyy");
                            userPhone.Value = user.Phone;
                            userEmail.Value = user.Email;
                            userFacebook.Value = user.FacebookAddress;
                            drpUserCity.SelectedValue = user.Locality != null ? user.Locality.Id.ToString() : "1";
                            drpUsertype.SelectedValue = ((int)user.UserType).ToString();
                            drpUserNationality.SelectedValue = ((int)user.Nationality).ToString();
                            userGender.SelectedValue = ((int)user.Gender).ToString();
                            btnSave.Text = "Actualizează datele utilizatorului";
                            lblUserId.Text = Request["userId"];
                            UserUsername.Value = user.Username;
                        }
                        else
                        {
                            Response.Redirect("~/Error.aspx?errorId=" + ErrorMessages.UserInvalid);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Error.aspx?errorId=" + ErrorMessages.UserInvalid);
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInputs())
            {
                return;
            }

            var sessionUser = Session[SessionConstants.LoginUser] as User;

            var user = new User
            {
                FirstName = userName.Value,
                LastName = userSurname.Value,
                HomeAddress = userAddress.Value,
                Birthdate = DateTime.ParseExact(userBirthday.Value, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture),
                Phone = userPhone.Value,
                Email = userEmail.Value,
                FacebookAddress = userFacebook.Value,
                JoinDate = DateTime.Now,
                Locality = new Locality
                {
                    Id = drpUserCity.SelectedValue.ToNullableInt().Value
                },
                Library = new Library
                {
                    Id = sessionUser.Library.Id
                },
                Flags = UserFlag.Default,
                UserType = (UserType)drpUsertype.SelectedValue.ToNullableInt(),
                Nationality = (Nationality)drpUserNationality.SelectedValue.ToNullableInt(),
                Gender = (Gender)userGender.SelectedValue.ToNullableInt(),
                Username = UserUsername.Value
            };

            if (!string.IsNullOrEmpty(lblUserId.Text) && lblUserId.Text.ToNullableInt() != 0)
            {
                user.Id = lblUserId.Text.ToNullableInt().Value;
                UsersManager.Update(user, sessionUser.Library.Id);
            }
            else
            {
                UsersManager.Add(user, sessionUser.Library.Id);
                lblStatus.Text = FlowMessages.UserAddSuccess;
                lblStatus.CssClass = "SuccessBox";
                CleanFields();
            }

            Response.Redirect("~/Users/Manage.aspx?Message=UserUpdatedSuccess");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Users/Manage.aspx");
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            lblUserType.Attributes.CssStyle.Add("color", "");
            lblUserUsername.Attributes.CssStyle.Add("color", "");

            if ((UserType)drpUsertype.SelectedValue.ToNullableInt() == UserType.Nespecificat)
            {
                lblUserType.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }

            if (string.IsNullOrEmpty(UserUsername.Value))
            {
                lblUserUsername.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }

            return isValid;
        }

        private void InitializeUserTypes()
        {
            var userTypes = EnumUtil.GetValues<UserType>().ToList();
            var listItemNationalities = userTypes.Select(c => new ListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToArray();

            drpUsertype.Items.AddRange(listItemNationalities);
            drpUsertype.DataBind();
        }

        private void InitializeLocalities()
        {
            var localities = LocalitiesManager.GetAllLocalities();
            var listItemLocalities = localities.Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToArray();

            drpUserCity.Items.AddRange(listItemLocalities);
            drpUserCity.DataBind();
        }

        private void InitializeNationalities()
        {
            var nationalities = EnumUtil.GetValues<Nationality>().ToList();
            var listItemNationalities = nationalities.Select(c => new ListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToArray();

            drpUserNationality.Items.AddRange(listItemNationalities);
            drpUserNationality.DataBind();
        }

        private void CleanFields()
        {
            userName.Value = string.Empty;
            userSurname.Value = string.Empty;
            userAddress.Value = string.Empty;
            userBirthday.Value = DateTime.Now.ToString();
            userPhone.Value = string.Empty;
            userEmail.Value = string.Empty;
            userFacebook.Value = string.Empty;
        }
    }
}
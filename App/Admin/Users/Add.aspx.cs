
namespace Admin.Users
{
    using BL.Constants;
    using BL.Helpers;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitializeNationalities();
            this.InitializeLocalities();
            this.InitializeUserTypes();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                FirstName = userName.Value,
                LastName = userSurname.Value,
                HomeAddress = userAddress.Value,
                Birthdate = DateTime.ParseExact(userBirthday.Value, "MM/dd/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture),
                Phone = userPhone.Value,
                Email = userEmail.Value,
                FacebookAddress = userFacebook.Value,
                JoinDate = DateTime.Now,
                Locality = new Locality
                {
                    Id = drpUserCity.SelectedValue.ToInt()
                },
                Flags = UserFlag.Default,
                UserType = (UserType)drpUsertype.SelectedValue.ToInt(),
                Nationality = (Nationality)drpUserNationality.SelectedValue.ToInt(),
                Gender = (Gender)userGender.SelectedValue.ToInt()
            };

            UsersManager.Add(user);

            lblStatus.Text = UserConstants.AddSuccess;
            lblStatus.CssClass = "SuccessBox";
            CleanFields();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
    }
}
﻿
namespace Admin.Helpers
{
    using System.ComponentModel;

    public enum ErrorMessages
    {
        BadRequest = 0,

        [Description("User inexistent, nu poți edita acest user!")]
        UserInvalid = 1,
    }
}
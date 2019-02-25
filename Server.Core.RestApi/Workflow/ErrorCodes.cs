using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.RestApi.Workflow
{
    /// <summary>
    /// Коды ошибок.
    /// </summary>
    public enum ErrorCodes
    {
        EmailExists = 100,

        WrongEmail = 101,

        WrongLogin = 102,

        LoginExists = 103,

        WrongPassword = 104,

        ErrorRegistration = 105,

        LoginNotExists = 106,

        ErrorChangePassword = 107,

        WrongNewPassword = 108,

        WrongOldPassword = 109,

        WrongRecoverNumber = 115,
    }
}
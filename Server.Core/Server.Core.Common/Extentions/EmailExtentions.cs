using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Server.Core.Common.Extentions
{
    public static class EmailExtantions
    {
        private const string EmailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase);
        }
    }
}

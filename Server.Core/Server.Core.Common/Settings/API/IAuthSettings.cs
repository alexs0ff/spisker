using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Common.Settings.API
{
    /// <summary>
    /// Настройки авторизации.
    /// </summary>
    public interface IAuthSettings:ISettings
    {
        string Issuer { get; }
        string Audiense { get; }
        string Key { get; }
        int Lifetime { get; }
    }
}

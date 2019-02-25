using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server.Core.Common.Settings.API;

namespace Server.Core.RestApi.Infrastructure.Settings
{
    public class AuthSettings: IAuthSettings
    {
        private readonly IConfiguration _configuration;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public AuthSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public string Issuer => _configuration["Auth:Issuer"];

        public string Audiense => _configuration["Auth:Audiense"];

        public string Key => _configuration["Auth:Key"];

        public int Lifetime => int.Parse(_configuration["Auth:Lifetime"]);
    }
}

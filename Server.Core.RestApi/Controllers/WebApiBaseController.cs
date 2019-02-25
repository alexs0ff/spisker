using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Server.Core.Common;
using Server.Core.Common.Repositories.Users;
using Server.Core.RestApi.Infrastructure.Auth;

namespace Server.Core.RestApi.Controllers
{
    /// <summary>
    /// Базовый контроллер для всех контроллеров WebApi.
    /// </summary>
    public class WebApiBaseController: Controller
    {
        protected string GetClientIp()
        {
            return Request?.HttpContext?.Connection?.RemoteIpAddress.ToString();
        }

        protected bool IsAuthenticated()
        {
            return User?.Identity?.IsAuthenticated ?? false;
        }

        protected IEnumerable<System.Security.Claims.Claim> GetClaims()
        {
            return User.Claims;
        }

        protected Guid? GetCurrentUserId()
        {
            var userId = User?.Claims?.Where(c => c.Type == CustomClaims.UserId).Select(i => Guid.Parse(i.Value))
                .FirstOrDefault();

            return userId;
        }

        protected string GetCurrentUser()
        {
            var userName = User?.Claims?.Where(c => c.Type == CustomClaims.UserName).Select(i=>i.Value).FirstOrDefault();

            return userName;
        }

        protected async Task<byte[]> ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    await ms.WriteAsync(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        protected string GetRootPath()
        {
            var hosting = StartEnumServer.Instance.Resolve<IHostingEnvironment>();
            return hosting.WebRootPath;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Server.Core.Common.Settings.API;

namespace Server.Core.RestApi.Infrastructure.Middlewares
{
    public class CorsOptionsMiddleware
    {
        private readonly RequestDelegate _next;

        private IHostingEnvironment _environment;

        private ICorsSettings _settings;

        private static HashSet<string> CorsUrls;

        private static char[] CorsSpliters = { ';', ',' };

        public CorsOptionsMiddleware(RequestDelegate next, IHostingEnvironment environment, ICorsSettings settings)
        {
            _next = next;
            _environment = environment;
            _settings = settings;
        }

        public async Task Invoke(HttpContext context)
        {
            var res = await BeginInvoke(context);
            if (res)
            {
                await _next.Invoke(context);
            }
        }

        private async Task<bool> BeginInvoke(HttpContext context)
        {
            if (CorsUrls == null)
            {
                if (string.IsNullOrWhiteSpace(_settings.ClientUrl))
                {
                    CorsUrls = new HashSet<string>();
                }
                else
                {
                    CorsUrls = new HashSet<string>(_settings.ClientUrl.Split(CorsSpliters, StringSplitOptions.RemoveEmptyEntries).Select(i => i.ToUpper()));
                }

            }

            var result = false;

            if (context.Request.Method == "OPTIONS")
            {
                AddCorsHeader(context);
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("OK");
            }
            else
            {
                AddCorsHeader(context);

                result = true;
            }

            return result;
        }

        private static void AddCorsHeader(HttpContext context)
        {
            var origin = context?.Request?.Headers["Origin"].ToString();

            if (string.IsNullOrWhiteSpace(origin))
            {
                return;
            }

            var key = origin.ToUpper();

            if (CorsUrls.Contains(key))
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", origin);
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, authorization");
            }

        }
    }
}

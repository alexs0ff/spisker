using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Server.Core.RestApi.Infrastructure.Middlewares
{
    public static class OptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsAndOptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsOptionsMiddleware>();
        }
    }
}

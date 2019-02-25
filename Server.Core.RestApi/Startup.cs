using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Server.Core.Common.Entities.Users;
using Server.Core.RestApi.Infrastructure;
using Server.Core.RestApi.Infrastructure.Auth;
using Server.Core.RestApi.Infrastructure.Middlewares;
using Server.Core.RestApi.Infrastructure.Settings;
using Server.Core.RestApi.Models.Auth;
using Server.Core.Users.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Server.Core.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //https://docs.microsoft.com/ru-ru/aspnet/core/security/authentication/identity-custom-storage-providers
            services.AddIdentity<Users.Auth.IdentityUser, ApplicationRole>(c =>
            {
                c.Password.RequireNonAlphanumeric = false;
                c.Password.RequiredLength = 8;
                c.Password.RequireDigit = true;
                c.Password.RequireUppercase = false;
                c.Password.RequireDigit = true;
            });


            var authSettings = new AuthSettings(Configuration);

            var keyData =  Encoding.UTF8.GetBytes(authSettings.Key);

            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                }).
                AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = authSettings.Issuer,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = authSettings.Audiense,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(keyData),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            
            services.AddTransient<IUserStore<Users.Auth.IdentityUser>, IdentityRepository>();
            services.AddMvc().AddJsonOptions(jo =>
            {
                jo.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "spisker.ru REST API",
                    Version = "v1",
                    Description = "Spisker.ru ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Kulik Alexander", Email = "alexsoff@yandex.ru", Url = "http://www.spisker.ru/alexsoff" },
                });
                
            });

            return ContainerConfig.Init(services,Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "spisker.ru API");
            });

            app.UseAuthentication();

            app.UseCorsAndOptions();
            
            app.UseMvc();
            
        }
    }
}

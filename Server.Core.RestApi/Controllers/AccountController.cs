using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Server.Core.Common.Extentions;
using Server.Core.Common.Messages;
using Server.Core.Common.Settings.API;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckRecaptcha;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.RestApi.Infrastructure.Auth;
using Server.Core.RestApi.Models.Auth;
using Server.Core.RestApi.Workflow.Account;
using Server.Core.RestApi.Workflow.Account.ChangePassword;
using Server.Core.RestApi.Workflow.Account.Register;
using Server.Core.RestApi.Workflow.Account.StartRecoverPassword;
using Server.Core.RestApi.Workflow.RecoverPassword;
using Server.Core.Users.Workflow.ChangeStatusText;
using Server.Core.Users.Workflow.GetAccountSettings;
using Server.Core.Users.Workflow.UpdateAccountAvatar;
using Server.Core.Users.Workflow.UpdateAccountSettings;

namespace Server.Core.RestApi.Controllers
{
    public class AccountController: WorkflowBaseController
    {
        /// <summary>
        /// Переопределение для создания workflow.
        /// </summary>
        /// <returns>Рабочий процесс.</returns>
        protected override WorkflowArea CreateWorkflow()
        {
            var workflow = new AccountWorkflow();

            return workflow.Create();
        }

        private readonly UserManager<Users.Auth.IdentityUser> _userManager;

        private readonly IAuthSettings _authSettings;

        public AccountController(UserManager<Users.Auth.IdentityUser> userManager, IAuthSettings authSettings)
        {
            _userManager = userManager;
            _authSettings = authSettings;
        }

        [HttpPost("/token")]
        public async Task Token()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            var identity = await GetIdentity(username, password);

            Response.ContentType = "application/json";

            if (identity == null)
            {
                Response.StatusCode = 400;
                
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _authSettings.Issuer,
                    audience: _authSettings.Audiense,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(_authSettings.Lifetime)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            var response = new Dictionary<string, string>();
            response.Add("access_token", encodedJwt);
            response.Add("expiresOn", jwt.ValidTo.ToJavaScriptDate().ToString(CultureInfo.InvariantCulture));

            identity.Claims.All(c => response.TryAdd(c.Type, c.Value));

            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented, ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user!=null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);
                if (result)
                {
                    
                        var claims = new List<Claim>
                        {
                            new Claim(CustomClaims.UserName, user.UserName.ToLower()),
                            new Claim(CustomClaims.UserEmail, user.Email),
                            new Claim(CustomClaims.UserId, user.PortalUserID.ToString())
                        };
                        ClaimsIdentity claimsIdentity =
                            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                                ClaimsIdentity.DefaultRoleClaimType);
                        return claimsIdentity;
                    
                }
            }

            // если пользователя не найдено
            return null;
        }

        /// <summary>
        /// Получение настроек определенного пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpGet]
        [Route("api/account/getsettings")]
        [Authorize]
        public async Task<MessageOutputBase> GetSettings(GetAccountSettingsRequest request)
        {
            MessageOutputBase result = null;

            request.UserName = GetCurrentUser();

            await Execute(flow =>
            {
                flow.StartRegisterFlow()
                    .Add<CheckUserExistsStep>()
                    .Add<GetAccountSettingsStep>();

                flow.
                    When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                    {
                        result = notFound.Response;
                    })
                    .When<GetAccountSettingsStep, GetAccountSettingsParams>(fetch =>
                    {
                        result = fetch.Response;
                    });

            }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Сохранение настроек определенного пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/account/updatesettings")]
        [Authorize]
        public async Task<MessageOutputBase> UpdateSettings([FromBody]UpdateAccountSettingsRequest request)
        {
            MessageOutputBase result = null;

            request.UserName = GetCurrentUser();

            await Execute(flow =>
            {
                flow.StartRegisterFlow()
                    .Add<CheckUserExistsStep>()
                    .Add<UpdateAccountSettingsStep>();

                flow.
                    When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                    {
                        result = notFound.Response;
                    })
                    .When<UpdateAccountSettingsStep, UpdateAccountSettingsParams>(update =>
                    {
                        result = update.Response;
                    });

            }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Изменение настроек пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/account/changepassword")]
        [Authorize]
        public async Task<MessageOutputBase> ChangePassword([FromBody]ChangePasswordRequest request)
        {
            MessageOutputBase result = null;

            request.UserName = GetCurrentUser();

            await Execute(flow =>
            {
                flow.StartRegisterFlow()
                    .Add<ChangePasswordStep>();

                flow.
                    When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                    {
                        result = notFound.Response;
                    })
                    .When<ChangePasswordStep, ChangePasswordParams>(change =>
                    {
                        result = change.Response;
                    });

            }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Изменение текстового статуса пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/account/changestatustext")]
        [Authorize]
        public async Task<MessageOutputBase> ChangeStatusText([FromBody]ChangeStatusTextRequest request)
        {
            MessageOutputBase result = null;

            request.UserName = GetCurrentUser();

            await Execute(flow =>
            {
                flow.StartRegisterFlow()
                    .Add<CheckUserExistsStep>()
                    .Add<ChangeStatusTextStep>();

                flow.
                    When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                    {
                        result = notFound.Response;
                    })
                    .When<ChangeStatusTextStep, ChangeStatusTextParams>(change =>
                    {
                        result = change.Response;
                    });

            }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Изменение аватара пользователя.
        /// </summary>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/account/changeavatar")]
        [Authorize]
        public async Task<MessageOutputBase> ChangeAvatar()
        {
            MessageOutputBase result = null;
            UpdateAccountAvatarRequest request = new UpdateAccountAvatarRequest();

            
            request.ClientIp = GetClientIp();
            request.RootPath = GetRootPath();

            if (Request.Form.Files.Count > 0)
            {
                request.FileName = Request.Form.Files[0].FileName;
                request.Data = await ReadFully(Request.Form.Files[0].OpenReadStream());
            }

            request.UserName = GetCurrentUser();

            await Execute(flow =>
            {
                flow.StartRegisterFlow()
                    .Add<CheckUserExistsStep>()
                    .Add<UpdateAccountAvatarStep>();

                flow.
                    When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                    {
                        result = notFound.Response;
                    })
                    .When<UpdateAccountAvatarStep, UpdateAccountAvatarParams>(change =>
                    {
                        result = change.Response;
                    });

            }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Старт выполнения задачи по восстановлению пароля.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/account/startrecoverpassword")]
        public async Task<MessageOutputBase> StartRecoverPassword([FromBody]StartRecoverPasswordRequest request)
        {
            MessageOutputBase result = null;


            request.ClientIp = GetClientIp();

            await Execute(flow =>
            {
                flow.StartRegisterFlow()
                    .Add<CheckRecaptchaStep>()
                    .Add<CheckUserExistsStep>()
                    .Add<StartRecoverPasswordStep>();

                flow.
                    When<CheckRecaptchaStep, CheckRecaptchaParams>(recaptcha =>
                    {
                        result = recaptcha.Response;
                    }).
                    When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                    {
                        result = notFound.Response;
                    })
                    .When<StartRecoverPasswordStep, StartRecoverPasswordParams>(recover =>
                    {
                        result = recover.Response;
                    });

            }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Восстановление пароля.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/account/recoverpassword")]
        public async Task<MessageOutputBase> RecoverPassword([FromBody]RecoverPasswordRequest request)
        {
            MessageOutputBase result = null;

            request.ClientIp = GetClientIp();

            await Execute(flow =>
            {
                flow.StartRegisterFlow()
                    .Add<CheckRecaptchaStep>()
                    .Add<CheckUserExistsStep>()
                    .Add<RecoverPasswordStep>();

                flow.
                    When<CheckRecaptchaStep, CheckRecaptchaParams>(recaptcha =>
                    {
                        result = recaptcha.Response;
                    }).
                    When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                    {
                        result = notFound.Response;
                    })
                    .When<RecoverPasswordStep, RecoverPasswordParams>(recover =>
                    {
                        result = recover.Response;
                    });

            }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Регистрация в системе пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/account/create")]
        public async Task<MessageOutputBase> Register([FromBody]AccountRegisterRequest request)
        {
            MessageOutputBase result = null;

            request.Ip = GetClientIp();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<AccountRegisterStep>();
                    flow.
                        When<AccountRegisterStep, AccountRegisterParams>(register =>
                        {
                            result = register.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }
    }
}

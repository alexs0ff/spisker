using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Server.Core.Common;
using Server.Core.Common.Entities.Social;
using Server.Core.Common.Extentions;
using Server.Core.Common.Logger;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;
using Server.Core.Users.Auth;

namespace Server.Core.RestApi.Workflow.Account.Register
{
    /// <summary>
    /// Шаг регистрации.
    /// </summary>
    public class AccountRegisterStep : StepBase<AccountRegisterParams>
    {
        private readonly UserManager<Users.Auth.IdentityUser> _userManager;

        public AccountRegisterStep()
        {
            _userManager = StartEnumServer.Instance.Resolve<UserManager<Users.Auth.IdentityUser>>();
        }

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(AccountRegisterParams state)
        {
            var logger = StartEnumServer.Instance.GetLogger();
            logger.LogInfo<AccountRegisterStep>(
                $"Старт обработки регистрации пользователя {state.Request.UserName} из ip={state.Request.Ip}",
                new LoggerParameter("ClientIp", state.Request.Ip), 
                new LoggerParameter("Login", state.Request.UserName),
                new LoggerParameter("Email", state.Request.Email));
            if (!await CheckPassword(state))
            {
                return Success();
            }

            if (!await CheckEmail(state))
            {
                return Success();
            }

            var user = new Users.Auth.IdentityUser
            {
                UserName = state.Request.UserName,
                Email = state.Request.Email,
                FirstName = state.Request.FirstName,
                LastName = state.Request.LastName,
                MiddleName = state.Request.MiddleName
            };
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                user.FirstName = String.Empty;
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                user.LastName = String.Empty;
            }

            if (!await CheckUser(state,user))
            {
                return Success();
            }

            var result = await _userManager.CreateAsync(user, state.Request.Password);

            var iProfileRepository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();

            var profile = new PortalUserProfile();
            profile.PortalUserID = user.PortalUserID;
            profile.StatusText = string.Empty;
            profile.PortalUserProfileID = Guid.NewGuid();

            await iProfileRepository.SaveNew(profile);

            if (result.Succeeded)
            {
                state.Response = new AccountRegistredResponse
                {
                    UserId = user.PortalUserID,
                    UserName = state.Request.UserName
                };
            }
            else
            {
                state.Response = new ErrorRegistrationResponse(string.Join(" ", result.Errors));
            }

            return Success();
        }
        
        /// <summary>
        /// Проверка пользователя.
        /// </summary>
        /// <param name="state">Состояние.</param>
        /// <returns>Результат.</returns>
        private async Task<bool> CheckUser(AccountRegisterParams state, Users.Auth.IdentityUser user)
        {
            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();
            var portalUser = await userRepository.GetByName(state.Request.UserName);
            if (portalUser != null)
            {
                state.Response = new LoginExistsResponse();
                return false;
            }

            var validator = _userManager.UserValidators.First();
            var result = await validator.ValidateAsync(_userManager,user);
            if (!result.Succeeded)
            {
                state.Response = new WrongLoginResponse(string.Join(" ", result.Errors));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка email.
        /// </summary>
        /// <param name="state">Состояние.</param>
        /// <returns>Результат.</returns>
        private async Task<bool> CheckEmail(AccountRegisterParams state)
        {
            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();

            if (string.IsNullOrWhiteSpace(state.Request.Email))
            {
                state.Response = new WrongEmailResponse();
                return false;
            }
            var user = await userRepository.FindByEmail(state.Request.Email);
            if (user != null)
            {
                state.Response = new EmailExistsResponse();
                return false;
            }


            bool isEmail = EmailExtantions.IsValidEmail(state.Request.Email);

            if (!isEmail)
            {
                state.Response = new WrongEmailResponse();
                return false;
            }

            return true;
        }

        private async Task<bool> CheckPassword(AccountRegisterParams state)
        {
            var validator = _userManager.PasswordValidators.First();
            var result = await validator.ValidateAsync(_userManager,new Users.Auth.IdentityUser{ UserName = state.Request.UserName}, state.Request.Password);
            if (!result.Succeeded)
            {
                state.Response = new WrongPasswordResponse(string.Join("; ", result.Errors));
                return false;
            }

            return true;
        }
    }
}
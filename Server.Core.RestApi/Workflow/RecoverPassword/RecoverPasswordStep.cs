using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Server.Core.Common;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;
using Server.Core.RestApi.Models.Auth;
using Server.Core.RestApi.Workflow.Account.ChangePassword;

namespace Server.Core.RestApi.Workflow.RecoverPassword
{
    /// <summary>
    /// Шаг восстановления пароля.
    /// </summary>
    public class RecoverPasswordStep: StepBase<RecoverPasswordParams>
    {
        private readonly UserManager<Users.Auth.IdentityUser> _userManager;

        public RecoverPasswordStep()
        {
            _userManager = StartEnumServer.Instance.Resolve<UserManager<Users.Auth.IdentityUser>>();
        }

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(RecoverPasswordParams state)
        {
            var logger = StartEnumServer.Instance.GetLogger();
            logger.LogInfo<RecoverPasswordStep>($"Старт смены пароля пользователем {state.User.UserName} c IP {state.ClientIp}");

            var recoverRepository = StartEnumServer.Instance.GetRepository<IRecoveryLoginRepository>();

            var recover = await recoverRepository.GetRecovery(state.User.UserName, state.Number);

            if (recover == null)
            {
                logger.LogError<RecoverPasswordStep>(
                    $"Нет номера восстановления для пользователя {state.User.UserName} c IP {state.ClientIp}; Номер {state.Number}");

                state.Response = new WrongNumberResponse();

                return Success();
            }

            if (recover.IsRecovered)
            {
                logger.LogError<RecoverPasswordStep>(
                    $"Номер восстановления уже использовался для пользователя {state.User.UserName} c IP {state.ClientIp}; Номер {state.Number}");

                state.Response = new WrongNumberResponse();

                return Success();
            }

            var passwordValid = await CheckPassword(state);

            if (!passwordValid)
            {
                return Success();
            }

            var appUser = await _userManager.FindByNameAsync(state.User.UserName);
            var hash = _userManager.PasswordHasher.HashPassword(appUser, state.NewPassword);
            

            var repository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();
            await repository.UpdatePasswordHash(state.User.PortalUserID, hash);

            var utc = DateTime.UtcNow;
            recover.IsRecovered = true;
            recover.LastUTCTryRecoverDateTime = utc;
            recover.UTCRecoveredDateTime = utc;

            await recoverRepository.Update(recover);

            state.Response = new RecoverPasswordResponse
            {
                UserName = state.User.UserName
            };

            logger.LogInfo<RecoverPasswordStep>($"Пароль для пользователя {state.User.UserName} c IP {state.ClientIp} успешно изменен");

            return Success();
        }

        private async Task<bool> CheckPassword(RecoverPasswordParams state)
        {
            var validator = _userManager.PasswordValidators.First();
            var appUser = await _userManager.FindByNameAsync(state.User.UserName);
            var result = await validator.ValidateAsync(_userManager,appUser, state.NewPassword);
            if (!result.Succeeded)
            {
                state.Response = new WrongNewPasswordResponse(string.Join("; ", result.Errors));
                return false;
            }

            return true;
        }
    }
}
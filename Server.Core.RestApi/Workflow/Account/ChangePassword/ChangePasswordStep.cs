using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Server.Core.Common;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.RestApi.Models.Auth;

namespace Server.Core.RestApi.Workflow.Account.ChangePassword
{
    /// <summary>
    /// Шаг смены пароля.
    /// </summary>
    public class ChangePasswordStep: StepBase<ChangePasswordParams>
    {
        private readonly UserManager<Users.Auth.IdentityUser> _userManager;

        public ChangePasswordStep()
        {
            _userManager = StartEnumServer.Instance.Resolve<UserManager<Users.Auth.IdentityUser>>();
        }

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(ChangePasswordParams state)
        {
            //Можно попробовать и так
            /*
             var _passwordValidator = 
                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
            var _passwordHasher =
                HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
     
            IdentityResult result = 
                await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
            if(result.Succeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
             
             
             */

            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();

            var user = await userRepository.GetByName(state.UserName);

            if (user ==null)
            {
                return ToFinish<UserNotFoundStep>();
            }
            
            var appUser = await _userManager.FindByNameAsync(user.UserName);

            if (appUser!=null)
            {
                if (await _userManager.CheckPasswordAsync(appUser, state.OldPassword))
                {
                    appUser = null;
                }
            }

            if (appUser==null)
            {
                state.Response = new WrongOldPasswordResponse();
                return Success();
            }

            var res = await CheckPassword(state);
            if (!res)
            {
                return Success();
            }

            var result = await _userManager.ChangePasswordAsync(appUser, state.OldPassword,
                state.NewPassword);
            
            if (result.Succeeded)
            {
                state.Response = new ChangePasswordResponse{UserName = user.UserName};
            }
            else
            {
                state.Response = new ErrorChangePasswordResponse(string.Join(" ", result.Errors));
            }
            return Success();
        }

        private async Task<bool> CheckPassword(ChangePasswordParams state)
        {
            var appUser = await _userManager.FindByNameAsync(state.UserName);
            var validator = _userManager.PasswordValidators.First();
            var result = await validator.ValidateAsync(_userManager, appUser, state.NewPassword);
            if (!result.Succeeded)
            {
                state.Response = new WrongNewPasswordResponse(string.Join("; ", result.Errors));
                return false;
            }

            return true;
        }
    }
}
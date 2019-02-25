using System.Threading.Tasks;
using Server.Core.Common.Repositories.Users;

namespace Server.Core.Common.Workflow.CheckUserExists
{
    /// <summary>
    /// Проверка на существование пользователя.
    /// </summary>
    public class CheckUserExistsStep : StepBase<CheckUserExistsParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Состояние перехода. если состояние не меняется, тогда исполнение прекращается.</returns>
        public override async Task<StepResult> Execute(CheckUserExistsParams state)
        {
            var userStore = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();

            var user = await userStore.GetByName(state.UserName);

            if (user != null)
            {
                state.User = user;
                return Success();
            }
            return ToFinish<UserNotFoundStep>();
        }
    }
}

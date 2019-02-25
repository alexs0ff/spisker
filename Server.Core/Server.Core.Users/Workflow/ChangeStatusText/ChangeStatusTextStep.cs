using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;

namespace Server.Core.Users.Workflow.ChangeStatusText
{
    /// <summary>
    /// Шаг для изменения статуса пользователя.
    /// </summary>
    public class ChangeStatusTextStep:StepBase<ChangeStatusTextParams>
    {
        private const int StatusMaxText = 500;

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(ChangeStatusTextParams state)
        {
            var profileRespository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();

            if (!string.IsNullOrWhiteSpace(state.NewStatus))
            {
                if (state.NewStatus.Length>StatusMaxText)
                {
                    state.NewStatus = state.NewStatus.Substring(StatusMaxText);
                }
            }

            await profileRespository.ChangeStatus(state.User.PortalUserID, state.NewStatus);

            state.Response = new ChangeStatusTextResponse();
            state.Response.UserName = state.User.UserName;
            state.Response.NewStatus = state.NewStatus;

            return Success();
        }
    }
}

using System;
using System.Threading.Tasks;
using Server.Core.Common.Repositories.Lists;

namespace Server.Core.Common.Workflow.CheckUserListExists
{
    /// <summary>
    /// Проверка на существование пользовательского списка.
    /// </summary>
    public class CheckUserListExistsStep:StepBase<CheckUserListExistsParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(CheckUserListExistsParams state)
        {
            Guid listId;

            if (!Guid.TryParse(state.ListId, out listId))
            {
                return ToFinish<UserListNotFoundStep>();
            }

            var exists = await StartEnumServer.Instance.GetRepository<IListRepository>()
                .ActiveListExists(listId, state.User.PortalUserID);

            if (!exists)
            {
                return ToFinish<UserListNotFoundStep>();
            }

            return Success();
        }
    }
}

using System;
using System.Threading.Tasks;
using Server.Core.Common.Repositories.Lists;

namespace Server.Core.Common.Workflow.CheckListExists
{
    /// <summary>
    /// Шаг на проверку существования списка.
    /// </summary>
    public class CheckListExistsStep:StepBase<CheckListExistsParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        public override async Task<StepResult> Execute(CheckListExistsParams state)
        {
            Guid listId;

            if (!Guid.TryParse(state.ListId,out listId))
            {
                return ToFinish<ListNotFoundStep>();
            }

            var exists = await StartEnumServer.Instance.GetRepository<IListRepository>().ActiveListExists(listId);

            if (!exists)
            {
                return ToFinish<ListNotFoundStep>();
            }

            return Success();
        }
    }
}

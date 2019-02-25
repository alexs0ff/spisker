using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Workflow;
using Server.Core.Lists.Workflow.UpdateListKind;

namespace Server.Core.Lists.Workflow.UpdatePublished
{
    /// <summary>
    /// Шаг обновления типа публикации списка.
    /// </summary>
    public class UpdatePublishedStep : StepBase<UpdatePublishedParams>
    {
        public override async Task<StepResult> Execute(UpdatePublishedParams state)
        {
            var loger = StartEnumServer.Instance.GetLogger();

            loger.LogInfo<UpdatePublishedStep>($"Обновление пользователем {state.User.PortalUserID}:{state.User.UserName} признака публикации списка {state.ListId} на {state.IsPublished}");
            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();
            await listRepository.UpdateListPublished(state.ListId, state.IsPublished);

            state.Response = new UpdatePublishedResponse
            {
                ListId = state.ListId,
                IsPublished = state.IsPublished
            };

            return Success();
        }
    }
}

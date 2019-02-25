using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Workflow;

namespace Server.Core.Users.Workflow.UpdateAccountAvatar
{
    /// <summary>
    /// Шаг по обновлению аватара.
    /// </summary>
    public class UpdateAccountAvatarStep:StepBase<UpdateAccountAvatarParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(UpdateAccountAvatarParams state)
        {
            var repository = StartEnumServer.Instance.GetRepository<IPortalUserProfileRespository>();
            var storage = StartEnumServer.Instance.GetFileStore();

            var result = await storage.Save(state.Data, state.FileName,state.RootPath, state.User.PortalUserID, state.ClientIp);

            state.Response = new UpdateAccountAvatarResponse
            {
                ImageUrl = result.FullUrl,
                UserName = state.User.UserName
            };

            var oldAvatarId = await repository.GetAvatarId(state.User.PortalUserID);

            await repository
                .ChangeAvatar(state.User.PortalUserID, result.FileId);

            if (oldAvatarId!=null)
            {
                await storage.DeleteFile(oldAvatarId.Value, state.RootPath, state.User.PortalUserID, state.ClientIp);
            }

            return Success();
        }
    }
}

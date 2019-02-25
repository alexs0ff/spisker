using System;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Social;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Repositories.Files;
using Server.Core.Common.Repositories.Social;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;
using Server.Core.Social.Models;

namespace Server.Core.Social.Workflow.Common
{
    /// <summary>
    /// Базовый шаг при работе с профилями.
    /// </summary>
    /// <typeparam name="TParameters"></typeparam>
    public abstract class ProfileStepBase<TParameters> : StepBase<TParameters> where TParameters : StepParameters, new()
    {
        /// <summary>
        /// Общий обработчик для получения модели профиля пользователя.
        /// </summary>
        /// <param name="profile">Найденный профиль.</param>
        /// <param name="portalUser">Пользователь.</param>
        /// <param name="currentUserName">Имя пользователя для сопряжения.</param>
        /// <returns>Модель профиля пользователя.</returns>
        protected async Task<PortalUserProfileModel> ProcessProfile(PortalUserProfile profile, PortalUser portalUser, string currentUserName)
        {
            var fileRepository = StartEnumServer.Instance.GetRepository<IStoredFileRepository>();

            var model = new PortalUserProfileModel();

            var mapper = StartEnumServer.Instance.GetMapper();
            mapper.Map(portalUser, model);

            if (profile != null)
            {
                mapper.Map(profile, model);
                if (profile.AvatarID != null)
                {
                    model.AvatarUrl = await fileRepository.GetFileUrl(profile.AvatarID.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(currentUserName) && !string.Equals(portalUser.UserName, currentUserName, StringComparison.OrdinalIgnoreCase))
            {
                var user = await StartEnumServer.Instance.GetRepository<IPortalUserRepository>()
                    .GetByName(currentUserName);
                if (user != null)
                {
                    model.IsFollowing = await StartEnumServer.Instance.GetRepository<IUserFollowingMapRepository>()
                        .IsFollowing(user.PortalUserID, portalUser.PortalUserID);
                }
            }

            return model;
        }
    }
}

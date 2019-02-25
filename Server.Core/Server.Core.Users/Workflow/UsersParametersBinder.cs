using AutoMapper;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Users.Workflow.ChangeStatusText;
using Server.Core.Users.Workflow.GetAccountSettings;
using Server.Core.Users.Workflow.UpdateAccountAvatar;
using Server.Core.Users.Workflow.UpdateAccountSettings;

namespace Server.Core.Users.Workflow
{
    /// <summary>
    /// Биндер для параметров рабочего процесса пользователей.
    /// </summary>
    public class UsersParametersBinder: StepParametersBinder
    {
        /// <summary>
        /// Должен переопределяться для правил биндинга параметров.
        /// </summary>
        /// <param name="mapperProfile">Профиль для регистрации.</param>
        public override void Configure(Profile mapperProfile)
        {
            BindGetAccountSettings(mapperProfile);
            BindUpdateAccountSettings(mapperProfile);
            BindChangeStatusText(mapperProfile);
            BindUpdateAccountAvatar(mapperProfile);
        }

        private void BindUpdateAccountAvatar(Profile mapperProfile)
        {
            mapperProfile.CreateMap<UpdateAccountAvatarRequest, CheckUserExistsParams>()
                .ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserExistsParams, UpdateAccountAvatarParams>()
                .ForMember(l => l.Data, c => c.MapFrom(i => ((UpdateAccountAvatarRequest) i.InputMessage).Data))
                .ForMember(l => l.ClientIp, c => c.MapFrom(i => ((UpdateAccountAvatarRequest) i.InputMessage).ClientIp))
                .ForMember(l => l.RootPath, c => c.MapFrom(i => ((UpdateAccountAvatarRequest) i.InputMessage).RootPath))
                .ForMember(l => l.FileName, c => c.MapFrom(i => ((UpdateAccountAvatarRequest) i.InputMessage).FileName));
        }

        private void BindChangeStatusText(Profile mapperProfile)
        {
            mapperProfile.CreateMap<ChangeStatusTextRequest, CheckUserExistsParams>()
                .ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserExistsParams, ChangeStatusTextParams>().
                ForMember(l => l.NewStatus, c => c.MapFrom(i => ((ChangeStatusTextRequest)i.InputMessage).NewStatus));
        }

        private void BindUpdateAccountSettings(Profile mapperProfile)
        {
            mapperProfile.CreateMap<UpdateAccountSettingsRequest, CheckUserExistsParams>()
                .ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserExistsParams, UpdateAccountSettingsParams>().
                ForMember(l=>l.Settings,c=>c.MapFrom(i=>((UpdateAccountSettingsRequest)i.InputMessage).Settings));
        }

        private void BindGetAccountSettings(Profile mapperProfile)
        {
            mapperProfile.CreateMap<GetAccountSettingsRequest, CheckUserExistsParams>()
                .ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserExistsParams, GetAccountSettingsParams>();
        }
    }
}

using AutoMapper;
using Server.Core.Common.Messages.Identifiable;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckRecaptcha;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.RestApi.Workflow.Account.ChangePassword;
using Server.Core.RestApi.Workflow.Account.Register;
using Server.Core.RestApi.Workflow.Account.StartRecoverPassword;
using Server.Core.RestApi.Workflow.RecoverPassword;

namespace Server.Core.RestApi.Workflow
{
    public class WorkflowBinder: StepParametersBinder
    {
        /// <summary>
        /// Должен переопределяться для правил биндинга параметров.
        /// </summary>
        /// <param name="mapperProfile">Профиль для регистрации.</param>
        public override void Configure(Profile mapperProfile)
        {
            BindCheckRecaptcha(mapperProfile);

            BindRegisterAccount(mapperProfile);
            BindChangePassword(mapperProfile);
            BindStartRecoverPassword(mapperProfile);
            BindRecoverPassword(mapperProfile);
        }

        private static void BindRecoverPassword(Profile mapperProfile)
        {
            mapperProfile.CreateMap<RecoverPasswordRequest, CheckRecaptchaParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckUserExistsParams, RecoverPasswordParams>().
                ForMember(m => m.ClientIp, i => i.MapFrom(l => ((RecoverPasswordRequest)l.InputMessage).ClientIp))
                .ForMember(m => m.NewPassword, i => i.MapFrom(l => ((RecoverPasswordRequest)l.InputMessage).NewPassword))
                .ForMember(m => m.Number, i => i.MapFrom(l => ((RecoverPasswordRequest)l.InputMessage).Number));
        }

        private static void BindStartRecoverPassword(Profile mapperProfile)
        {
            mapperProfile.CreateMap<StartRecoverPasswordRequest, CheckRecaptchaParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckUserExistsParams, StartRecoverPasswordParams>().
                ForMember(m => m.ClientIp, i => i.MapFrom(l => ((StartRecoverPasswordRequest)l.InputMessage).ClientIp));
        }

        private static void BindCheckRecaptcha(Profile mapperProfile)
        {
            mapperProfile.CreateMap<CheckRecaptchaParams, CheckUserExistsParams>().
                ForMember(i=>i.UserName,l=>l.MapFrom(m=>((IUserName)m.InputMessage).UserName));
        }

        private static void BindRegisterAccount(Profile mapperProfile)
        {
            mapperProfile.CreateMap<AccountRegisterRequest, AccountRegisterParams>().
                ForMember(m => m.Request, i => i.MapFrom(l => l));
        }

        private static void BindChangePassword(Profile mapperProfile)
        {
            mapperProfile.CreateMap<ChangePasswordRequest, ChangePasswordParams>();
            mapperProfile.CreateMap<ChangePasswordParams, UserNotFoundParams>().
                ForMember(i=>i.Response,c=>c.Ignore());
        }
    }
}
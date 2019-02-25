using AutoMapper;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckListExists;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Social.Workflow.FindUsers;
using Server.Core.Social.Workflow.GetFollowings;
using Server.Core.Social.Workflow.GetProfile;
using Server.Core.Social.Workflow.GetWhoFollow;
using Server.Core.Social.Workflow.RepostList;
using Server.Core.Social.Workflow.SetLikeList;
using Server.Core.Social.Workflow.StartFollowing;
using Server.Core.Social.Workflow.StopFollowing;
using Server.Core.Social.Workflow.UnsetLikeList;

namespace Server.Core.Social.Workflow
{
    /// <summary>
    /// Биндер для социальных операций.
    /// </summary>
    public class SocialParametersBinder : StepParametersBinder
    {
        /// <summary>
        /// Должен переопределяться для правил биндинга параметров.
        /// </summary>
        /// <param name="mapperProfile">Профиль для регистрации.</param>
        public override void Configure(Profile mapperProfile)
        {
            BindStartFollowing(mapperProfile);
            BindStopFollowing(mapperProfile);
            BindGetProfile(mapperProfile);
            BindSetLikeList(mapperProfile);
            BindUnsetLikeList(mapperProfile);
            BindFindUsers(mapperProfile);
            BindRepostList(mapperProfile);
            BindWhoFollow(mapperProfile);
            BindGetFollowings(mapperProfile);
        }
        
        private static void BindGetFollowings(Profile mapperProfile)
        {
            mapperProfile.CreateMap<GetFollowingsRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckUserExistsParams, GetFollowingsParams>()
                .ForMember(m => m.CurrentUserName,
                    i => i.MapFrom(l => ((GetFollowingsRequest)l.InputMessage).CurrentUserName))
                .ForMember(m => m.Search,
                    i => i.MapFrom(l => ((GetFollowingsRequest)l.InputMessage).Search))
                .ForMember(m => m.LastFollowingId,
                    i => i.MapFrom(l => ((GetFollowingsRequest)l.InputMessage).LastFollowingId));
        }

        private static void BindWhoFollow(Profile mapperProfile)
        {
            mapperProfile.CreateMap<GetWhoFollowRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckUserExistsParams, GetWhoFollowParams>()
                .ForMember(m => m.CurrentUserName,
                    i => i.MapFrom(l => ((GetWhoFollowRequest) l.InputMessage).CurrentUserName))
                .ForMember(m => m.Search,
                    i => i.MapFrom(l => ((GetWhoFollowRequest) l.InputMessage).Search))
                .ForMember(m => m.LastFollowerId,
                    i => i.MapFrom(l => ((GetWhoFollowRequest) l.InputMessage).LastFollowerId));
        }

        private static void BindFindUsers(Profile mapperProfile)
        {
            mapperProfile.CreateMap<FindUsersRequest, FindUsersParams>();
        }

        private static void BindRepostList(Profile mapperProfile)
        {
            mapperProfile.CreateMap<RepostListRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckListExistsParams, RepostListParams>();
        }

        private static void BindUnsetLikeList(Profile mapperProfile)
        {
            mapperProfile.CreateMap<UnsetLikeListRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckListExistsParams, UnsetLikeListParams>();
        }

        private static void BindSetLikeList(Profile mapperProfile)
        {
            mapperProfile.CreateMap<SetLikeListRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckListExistsParams, SetLikeListParams>();
        }

        private void BindGetProfile(Profile mapperProfile)
        {
            mapperProfile.CreateMap<GetProfileRequest, CheckUserExistsParams>()
                .ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
                
            mapperProfile.CreateMap<CheckUserExistsParams, GetProfileParams>()
                .ForMember(m => m.CurrentUserName, i => i.MapFrom(l => ((GetProfileRequest)l.InputMessage).CurrentUserName));
        }

        private static void BindStartFollowing(Profile mapperProfile)
        {
            mapperProfile.CreateMap<StartFollowingRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckUserExistsParams, StartFollowingParams>().
                ForMember(m=>m.ToUserName,i=>i.MapFrom(l=>((StartFollowingRequest)l.InputMessage).ToUserName));

            mapperProfile.CreateMap<StartFollowingParams, UserNotFoundParams>()
                .ForMember(m => m.UserName, i => i.MapFrom(l => l.ToUserName));
        }

        private static void BindStopFollowing(Profile mapperProfile)
        {
            mapperProfile.CreateMap<StopFollowingRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));
            mapperProfile.CreateMap<CheckUserExistsParams, StopFollowingParams>().
                ForMember(m => m.ToUserName, i => i.MapFrom(l => ((StopFollowingRequest)l.InputMessage).ToUserName));

            mapperProfile.CreateMap<StopFollowingParams, UserNotFoundParams>()
                .ForMember(m => m.UserName, i => i.MapFrom(l => l.ToUserName));
        }
    }
}

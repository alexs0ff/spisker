using Server.Core.Common.Entities.Social;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Mapper;
using Server.Core.Social.Models;
using Server.Core.Social.Workflow;

namespace Server.Core.Social
{
    /// <summary>
    /// Социальный профиль.
    /// </summary>
    public class SocialMapperProfile: MapperBaseProfile
    {
        public SocialMapperProfile()
        {
            RegisterWorkflowParams<SocialParametersBinder>();

            CreateMap<PortalUserProfile, PortalUserProfileModel>();
            CreateMap<PortalUser, PortalUserProfileModel>();
        }
    }
}

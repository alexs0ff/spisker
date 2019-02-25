using Server.Core.Common.Mapper;
using Server.Core.Users.Workflow;

namespace Server.Core.Users
{
    /// <summary>
    /// Профиль для пользователей.
    /// </summary>
    public class UsersMapperProfile: MapperBaseProfile
    {
        public UsersMapperProfile()
        {
            RegisterWorkflowParams<UsersParametersBinder>();
        }
    }
}

using Server.Core.Common.GraphQL;

namespace Server.Core.Social.GraphQL
{
    /// <summary>
    /// Резолвер для поиска пользователей.
    /// </summary>
    public interface IGetUsersResolver : ITypeResolver<object>
    {
    }
}

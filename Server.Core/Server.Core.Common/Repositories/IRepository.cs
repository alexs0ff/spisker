using Server.Core.Common.Repositories;

namespace Server.Core.Common.Repositories
{
    /// <summary>
    /// Интерфейс для всех репозиториев.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Забирает единый контекст из другого репозитория.
        /// </summary>
        /// <param name="repository">Репозиторий от куда расшарить контекст.</param>
        void ShareContext(IRepository repository);

        /// <summary>
        /// Начать транзакцию в контексте.
        /// </summary>
        RepositoryTransaction GetTransaction();
    }
}

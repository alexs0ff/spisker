using System;
using System.Threading.Tasks;
using Server.Core.Common.Entities.Users;

namespace Server.Core.Common.Repositories.Users
{
    /// <summary>
    /// Репозиторий для информации по восстановлению логинов.
    /// </summary>
    public interface IRecoveryLoginRepository: ICrudRepository<RecoveryLogin, Guid>
    {
        /// <summary>
        /// Получает сущность восстановления.
        /// </summary>
        /// <param name="userLogin">Логин пользователя.</param>
        /// <param name="number">Номер восстановления.</param>
        /// <returns>Результат.</returns>
        Task<RecoveryLogin> GetRecovery(string userLogin, string number);
    }
}

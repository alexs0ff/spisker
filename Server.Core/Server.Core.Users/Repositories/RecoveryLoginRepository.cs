using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Contexts;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Repositories;
using Server.Core.Common.Repositories.Users;

namespace Server.Core.Users.Repositories
{
    /// <summary>
    /// Реализация репозитория по восстановлению логинов и паролей.
    /// </summary>
    public class RecoveryLoginRepository : CrudRepositoryBase<RecoveryLogin, Guid, StartenumEntities, StartenumEntitiesFactory>, IRecoveryLoginRepository
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public RecoveryLoginRepository(StartenumEntitiesFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Получает сущность восстановления.
        /// </summary>
        /// <param name="userLogin">Логин пользователя.</param>
        /// <param name="number">Номер восстановления.</param>
        /// <returns>Результат.</returns>
        public Task<RecoveryLogin> GetRecovery(string userLogin, string number)
        {
            return Entities.Where(i => i.LoginName == userLogin && i.SentNumber == number)
                .FirstOrDefaultAsync();
        }
    }
}

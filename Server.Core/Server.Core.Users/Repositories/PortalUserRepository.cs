using System;
using System.Collections.Generic;
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
    /// Реализация репозитория для пользователя.
    /// </summary>
    public class PortalUserRepository: CrudRepositoryBase<PortalUser, Guid, StartenumEntities, StartenumEntitiesFactory>, IPortalUserRepository
       
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public PortalUserRepository(StartenumEntitiesFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Поиск пользователя по имени.
        /// </summary>
        /// <param name="userName">Имя пользователя..</param>
        /// <returns>Пользователь</returns>
        public async Task<PortalUser> GetByName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }
            userName = userName.ToUpper();
            var us = await Entities.Where(i => i.UserName.ToUpper() == userName).FirstOrDefaultAsync();

            return us;
        }

        /// <summary>
        /// Поиск пользователя по email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Результат.</returns>
        public async Task<PortalUser> FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            email = email.ToUpper();
            var user = await Entities.Where(u => u.Email.ToUpper() == email).FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// Получение пользователя по его коду.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Пользователь.</returns>
        public IQueryable<PortalUser> GetPortalUsers(Guid userId)
        {
            return Entities.Where(u => u.PortalUserID == userId);
        }

        /// <summary>
        /// Обновляет имя пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Результат.</returns>
        public Task UpdateFirstName(Guid userId,string value)
        {
            var sql = @"UPDATE [dbo].[PortalUser]
                   SET [FirstName] = {1}      
                 WHERE PortalUserID  = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(sql, userId, value);
        }

        /// <summary>
        /// Обновляет фамилию пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Результат.</returns>
        public Task UpdateLastName(Guid userId, string value)
        {
            var sql = @"UPDATE [dbo].[PortalUser]
                   SET [LastName] = {1}      
                 WHERE PortalUserID  = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(sql, userId, value);
        }

        /// <summary>
        /// Обновляет отчество пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Результат.</returns>
        public Task UpdateMiddleName(Guid userId, string value)
        {
            var sql = @"UPDATE [dbo].[PortalUser]
                   SET [MiddleName] = {1}      
                 WHERE PortalUserID  = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(sql, userId, value);
        }

        /// <summary>
        /// Обновляет email пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Результат.</returns>
        public Task UpdateEmail(Guid userId, string value)
        {
            var sql = @"UPDATE [dbo].[PortalUser]
                   SET [Email] = {1}      
                 WHERE PortalUserID  = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(sql, userId, value);
        }

        /// <summary>
        /// Производит поиск пользователей по вхождению строки.
        /// </summary>
        /// <param name="name">Строка вхождения.</param>
        /// <param name="count">Максимальное количество возвращаемых строк.</param>
        /// <returns>Списко найденных пользователей.</returns>
        public IEnumerable<PortalUser> GetUsers(string name,int count)
        {
            name = name.ToUpper();

            return Entities.Where(u => u.UserName.ToUpper().StartsWith(name) ||
                                       u.FirstName.ToUpper().StartsWith(name)||
                                       u.LastName.ToUpper().StartsWith(name)
                                       ).Take(count);
        }

        /// <summary>
        /// Обновляет хэш пароля пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="newPasswordHash">Новый хэ пароля..</param>
        /// <returns>Результат.</returns>
        public Task UpdatePasswordHash(Guid userId, string newPasswordHash)
        {
            var sql = @"UPDATE [dbo].[PortalUser]
                   SET [PasswordHash] = {1}      
                 WHERE PortalUserID  = {0}";

            return GetContext().Database.ExecuteSqlCommandAsync(sql, userId, newPasswordHash);
        }
    }
}

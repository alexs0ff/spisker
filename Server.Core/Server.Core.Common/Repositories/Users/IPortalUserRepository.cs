using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Core.Common.Entities.Users;

namespace Server.Core.Common.Repositories.Users
{
    /// <summary>
    /// Интерфейс репозитория для пользователя.
    /// </summary>
    public interface IPortalUserRepository: ICrudRepository<PortalUser, Guid>
    {
        /// <summary>
        /// Поиск пользователя по имени.
        /// </summary>
        /// <param name="userName">Имя пользователя..</param>
        /// <returns>Пользователь</returns>
        Task<PortalUser> GetByName(string userName);

        /// <summary>
        /// Поиск пользователя по email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Результат.</returns>
        Task<PortalUser> FindByEmail(string email);

        /// <summary>
        /// Получение пользователя по его коду.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <returns>Пользователь.</returns>
        IQueryable<PortalUser> GetPortalUsers(Guid userId);

        /// <summary>
        /// Обновляет email пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Результат.</returns>
        Task UpdateEmail(Guid userId, string value);

        /// <summary>
        /// Обновляет отчество пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Результат.</returns>
        Task UpdateMiddleName(Guid userId, string value);

        /// <summary>
        /// Обновляет фамилию пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Результат.</returns>
        Task UpdateLastName(Guid userId, string value);

        /// <summary>
        /// Обновляет имя пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="value">Новое значение.</param>
        /// <returns>Результат.</returns>
        Task UpdateFirstName(Guid userId,string value);

        /// <summary>
        /// Производит поиск пользователей по вхождению строки.
        /// </summary>
        /// <param name="name">Строка вхождения.</param>
        /// <param name="count">Максимальное количество возвращаемых строк.</param>
        /// <returns>Списко найденных пользователей.</returns>
        IEnumerable<PortalUser> GetUsers(string name,int count);

        /// <summary>
        /// Обновляет хэш пароля пользователя.
        /// </summary>
        /// <param name="userId">Код пользователя.</param>
        /// <param name="newPasswordHash">Новый хэ пароля..</param>
        /// <returns>Результат.</returns>
        Task UpdatePasswordHash(Guid userId, string newPasswordHash);
    }
}

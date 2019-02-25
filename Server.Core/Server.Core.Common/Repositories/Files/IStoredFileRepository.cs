using System;
using System.Threading.Tasks;
using Server.Core.Common.Entities.Files;

namespace Server.Core.Common.Repositories.Files
{
    /// <summary>
    /// Репозиторий работы с картинками.
    /// </summary>
    public interface IStoredFileRepository : ICrudRepository<StoredFile, Guid>
    {
        /// <summary>
        /// Получает путь к файлу.
        /// </summary>
        /// <param name="fileId">Код файла.</param>
        /// <returns>Результат.</returns>
        Task<string> GetFileUrl(Guid fileId);

        /// <summary>
        /// Проверяет на существование файл в хранилище.
        /// </summary>
        /// <param name="fileId">Код файла.</param>
        /// <returns>Результат.</returns>
        Task<bool> FileExists(Guid fileId);
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Contexts;
using Server.Core.Common.Entities.Files;
using Server.Core.Common.Repositories;
using Server.Core.Common.Repositories.Files;

namespace Server.Core.Files.Repositories
{
    /// <summary>
    /// Реализация репозитория хранения картинок.
    /// </summary>
    public class StoredFileRepository : CrudRepositoryBase<StoredFile, Guid, StartenumEntities, StartenumEntitiesFactory>, IStoredFileRepository
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public StoredFileRepository(StartenumEntitiesFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Получает путь к файлу.
        /// </summary>
        /// <param name="fileId">Код файла.</param>
        /// <returns>Результат.</returns>
        public Task<string> GetFileUrl(Guid fileId)
        {
            return Entities.Where(i => i.StoredFileID == fileId).Select(i => i.StoreAddress).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Проверяет на существование файл в хранилище.
        /// </summary>
        /// <param name="fileId">Код файла.</param>
        /// <returns>Результат.</returns>
        public Task<bool> FileExists(Guid fileId)
        {
            return Entities.AnyAsync(i => i.StoredFileID == fileId);
        }
    }
}

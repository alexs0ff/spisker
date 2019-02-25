using System;
using System.Threading.Tasks;
using Server.Core.Common.Services;

namespace Server.Core.Common.Files
{
    /// <summary>
    /// Сервис для управления сохраненными файлами.
    /// </summary>
    public interface IFileStoreService: IServiceBase
    {
        /// <summary>
        /// Сохранение данных в файл.
        /// </summary>
        /// <param name="data">Сохраняемые данные.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="rootPath">Первоначальный путь.</param>
        /// <param name="userId">Код загрузившего пользователя файла.</param>
        /// <param name="clientIp">IP откуда поступил запрос.</param>
        /// <returns>Результат сохранения.</returns>
        Task<FileSaveResult> Save(byte[] data,string fileName,string rootPath,Guid userId, string clientIp);

        /// <summary>
        /// Удаляет из хранилища файл.
        /// </summary>
        /// <param name="storedFileId">КОд хранимого файла.</param>
        /// <param name="rootPath">Первоначальный путь</param>
        /// <param name="userId">Код загрузившего пользователя файла.</param>
        /// <param name="clientIp">IP откуда поступил запрос.</param>
        /// <returns>Результат.</returns>
        Task<bool> DeleteFile(Guid storedFileId, string rootPath, Guid userId, string clientIp);
    }
}

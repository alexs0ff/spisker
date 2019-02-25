using System;
using System.IO;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Files;
using Server.Core.Common.Files;
using Server.Core.Common.Logger;
using Server.Core.Common.Repositories.Files;
using Server.Core.Common.Settings.Files;

namespace Server.Core.Files
{
    /// <summary>
    /// Реализация сервиса файлового хранилища.
    /// </summary>
    public class FileStoreService: IFileStoreService
    {
        private readonly IFilesSettings _filesSettings;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public FileStoreService()
        {
            _filesSettings = StartEnumServer.Instance.GetSettings<IFilesSettings>();
        }

        /// <summary>
        /// Получает url для сохраненной сущности файла.
        /// </summary>
        /// <param name="fileName">Имя сохраненного файла.</param>
        /// <param name="id">Код файла.</param>
        /// <returns>Путь.</returns>
        public string GetServerFilePath(Guid id,string fileName)
        {
            return _filesSettings.ServerPath1 + id + Path.GetExtension(fileName);
        }

        /// <summary>
        /// Получает url для сохраненной сущности файла в локальной папке.
        /// </summary>
        /// <param name="fileName">Имя сохраненного файла.</param>
        /// <param name="id">Код файла.</param>
        /// <returns>Путь.</returns>
        public string GetLocalFilePath(Guid id, string fileName)
        {
            return _filesSettings.LocalPath1 + id + Path.GetExtension(fileName);
        }

        /// <summary>
        /// Получает url для сохраненной сущности файла в локальной папке.
        /// </summary>
        /// <param name="fileName">Имя сохраненного файла.</param>
        /// <param name="id">Код файла.</param>
        /// <param name="rootPath">Первоначальный путь.</param>
        /// <returns>Путь.</returns>
        public string GetLocalFilePath(Guid id, string fileName,string rootPath)
        {
            var local = GetLocalFilePath(id, fileName);

            return Path.Combine(rootPath, local);
        }

        /// <summary>
        /// Сохранение данных в файл.
        /// </summary>
        /// <param name="data">Сохраняемые данные.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="rootPath">Первоначальный путь.</param>
        /// <param name="userId">Код загрузившего пользователя файла.</param>
        /// <param name="clientIp">IP откуда поступил запрос.</param>
        /// <returns>Результат сохранения.</returns>
        public async Task<FileSaveResult> Save(byte[] data,string fileName,string rootPath,Guid userId, string clientIp)
        {
            var logger = StartEnumServer.Instance.GetLogger();
            var id = Guid.NewGuid();

            var ident = new []
            {
                new LoggerParameter("StoredFileId", id),
                new LoggerParameter("Ip", clientIp),
                new LoggerParameter("fileName", fileName),
                new LoggerParameter("userId", userId),
                new LoggerParameter("fileSize", data.LongLength.ToString())
            };
            logger.LogInfo<FileStoreService>($"Старт сохранения файла {fileName}:{id} размером {data.Length} из {clientIp} пользователем {userId}",ident);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                var message = $"Ошибка сохранения пустого имени файла {fileName}:{id} размером {data.Length} из {clientIp} пользователем {userId}";
                logger.LogError<FileStoreService>(message, ident);
                throw new Exception(message);
            }

            if (data.LongLength>_filesSettings.MaxSize)
            {
                var message = $"Размер файла превышает {_filesSettings.MaxSize} {fileName}:{id} размером {data.Length} из {clientIp} пользователем {userId}";
                logger.LogError<FileStoreService>(message, ident);
                throw new Exception(message);
            }
            
            var storedFile = new StoredFile();
            storedFile.CreateDate = DateTime.UtcNow;
            storedFile.Extention = Path.GetExtension(fileName);
            storedFile.FileName = fileName;
            storedFile.FileSize = data.LongLength;
            storedFile.UserID = userId;
            storedFile.StoreAddress = GetServerFilePath(id, fileName);
            storedFile.StoredFileID = id;

            var local = GetLocalFilePath(id, fileName, rootPath);
            
            var repository = StartEnumServer.Instance.GetRepository<IStoredFileRepository>();

            using (var trans = repository.GetTransaction())
            {
                trans.Begin();

                await repository.SaveNew(storedFile);

                using (var fs = new FileStream(local, FileMode.Create, FileAccess.Write))
                {
                    await fs.WriteAsync(data, 0, data.Length);
                    fs.Close();
                }

                trans.Commit();
            }

            return new FileSaveResult
            {
                FileId = id,
                FullUrl = storedFile.StoreAddress
            };
        }

        /// <summary>
        /// Удаляет из хранилища файл.
        /// </summary>
        /// <param name="storedFileId">КОд хранимого файла.</param>
        /// <param name="rootPath">Первоначальный путь</param>
        /// <param name="userId">Код загрузившего пользователя файла.</param>
        /// <param name="clientIp">IP откуда поступил запрос.</param>
        /// <returns>Результат.</returns>
        public async Task<bool> DeleteFile(Guid storedFileId, string rootPath, Guid userId, string clientIp)
        {
            var logger = StartEnumServer.Instance.GetLogger();
            var repository = StartEnumServer.Instance.GetRepository<IStoredFileRepository>();

            var ident = new[]
            {
                new LoggerParameter("StoredFileId", storedFileId),
                new LoggerParameter("Ip", clientIp),
                new LoggerParameter("userId", userId),
            };

            logger.LogInfo<FileStoreService>($"Старт удаления файла {storedFileId} из {clientIp} пользователем {userId}", ident);

            var file = await repository.GetById(storedFileId);

            
            if (file==null)
            {
                logger.LogInfo<FileStoreService>($"Файл в хранилище не найден {storedFileId} из {clientIp} пользователь {userId}", ident);
                return false;
            }

            var local = GetLocalFilePath(storedFileId, file.FileName, rootPath);

            
            using (var trans = repository.GetTransaction())
            {
                trans.Begin();

                await repository.Delete(storedFileId);

                if (File.Exists(local))
                {
                    File.Delete(local);
                }

                trans.Commit();
            }

            return true;
        }
    }
}

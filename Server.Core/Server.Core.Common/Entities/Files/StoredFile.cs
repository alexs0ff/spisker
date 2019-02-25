using System;

namespace Server.Core.Common.Entities.Files
{
    /// <summary>
    /// Сохраненный файл на сервере.
    /// </summary>
    public class StoredFile : EntityBase<Guid>
    {
        public System.Guid StoredFileID { get; set; }
        public string StoreAddress { get; set; }
        public string FileName { get; set; }
        public string Extention { get; set; }
        public long FileSize { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.Guid> UserID { get; set; }

        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public override Guid GetId()
        {
            return StoredFileID;
        }
    }
}

using System;

namespace Server.Core.Common.Entities.Users
{
    public class RecoveryLogin : EntityBase<Guid>
    {
        public System.Guid RecoveryLoginID { get; set; }
        public string LoginName { get; set; }
        public System.DateTime UTCEventDateTime { get; set; }
        public System.DateTime UTCEventDate { get; set; }
        public string RecoveryEmail { get; set; }
        public string RecoveryClientIdentifier { get; set; }
        public bool IsRecovered { get; set; }
        public string RecoveredClientIdentifier { get; set; }
        public string SentNumber { get; set; }
        public Nullable<System.DateTime> UTCRecoveredDateTime { get; set; }
        public int TryCount { get; set; }
        public Nullable<System.DateTime> LastUTCTryRecoverDateTime { get; set; }

        /// <summary>
        /// Получает ключ.
        /// </summary>
        /// <returns>Значение ключа сущности.</returns>
        public override Guid GetId()
        {
            return RecoveryLoginID;
        }
    }
}

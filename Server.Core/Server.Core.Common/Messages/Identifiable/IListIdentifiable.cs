namespace Server.Core.Common.Messages.Identifiable
{
    /// <summary>
    /// Идентифицируемые сообщения по коду списка.
    /// </summary>
    public interface IListIdentifiable
    {
        /// <summary>
        /// Код списка.
        /// </summary>
        string ListId { get; set; }
    }
}

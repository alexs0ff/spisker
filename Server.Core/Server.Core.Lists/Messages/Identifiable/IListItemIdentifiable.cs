namespace Server.Core.Lists.Messages.Identifiable
{
    /// <summary>
    /// Идентификация пункта списка.
    /// </summary>
    public interface IListItemIdentifiable
    {
        /// <summary>
        /// Код пункта списка.
        /// </summary>
        string ListItemId { get; set; }
    }
}

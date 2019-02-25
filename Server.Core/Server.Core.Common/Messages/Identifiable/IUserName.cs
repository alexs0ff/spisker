namespace Server.Core.Common.Messages.Identifiable
{
    /// <summary>
    /// Интерфейс для определения пользователей.
    /// </summary>
    public interface IUserName
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        string UserName { get; set; }
    }
}

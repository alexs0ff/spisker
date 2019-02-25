namespace Server.Core.Common.Messages
{
    /// <summary>
    /// Общие ошибки
    /// </summary>
    public enum CommonErrors
    {
        UserNotFound = 5,

        ListNotFound = 10,

        ListItemNotFound = 15,

        UserListNotFound = 20,

        InvalidRecaptcha = 25,

        SystemError = 500,

        Unauthorized = 401
}
}

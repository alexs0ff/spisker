using Server.Core.Common.Messages;

namespace Server.Core.Lists.Messages
{
    /// <summary>
    /// Коды ошибок работы со списком.
    /// </summary>
    public enum ErrorCodes
    {
        UserNotFound = CommonErrors.UserNotFound,

        ListNotFound = CommonErrors.ListNotFound,
        
        ListItemNotFound = CommonErrors.ListItemNotFound,

        ListKindIdIsNotExists = 100,

        ListCheckItemKindIsIsNotExists = 110
    }
}

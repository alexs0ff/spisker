export enum AccountEventType {
    //Получены настройки аккаунта.
    AccountSettingsReceived,

    ///Настроки аккаунта обновлены
    AccountSettingsUpdated,

    ///Пароль изменен
    PasswordChanged,

    ///Ошибки изменения пароля
    PasswordChangeError,

    ///Измененный текст статуса
    StatusTextChanged,

    //Измененный аватар
    AvatarChanged
}
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.RestApi.Workflow.Account.StartRecoverPassword
{
    /// <summary>
    /// Шаг начала восстановления пароля.
    /// </summary>
    public class StartRecoverPasswordStep:StepBase<StartRecoverPasswordParams>
    {

        /// <summary>
        /// Содержит формат тела сообщения регистрации.
        /// </summary>
        private const string RecoveryMessageBodyFormat = @"
Здравствуйте, {0}!

Вы получили это письмо, потому что Ваш адрес электронной почты был указан при процедуре восстановления пароля на сайте www.spisker.ru

Чтобы подтвердить восстановление пароля, перейдите по этой ссылке. 

{1}
Если ссылка не открывается, скопируйте её и вставьте в адресную строку браузера.

Подтверждение необходимо для исключения несанкционированного изменения Вашего пароля. 
";

        /// <summary>
        /// Содержит формат заголовка сообщения регистрации.
        /// </summary>
        private const string RecoveryMessageTitleFormat = "Пожалуйста, подтвердите восстановление пароля";

#if DEBUG

        /// <summary>
        /// Содержит формат utl для активации.
        /// </summary>
        private const string RecoveryUrlFormat = "http://localhost:5296/i/account/approve/{0}/{1}";
#else
        private const string RecoveryUrlFormat = "http://www.spisker.ru/i/account/approve/{0}/{1}";
#endif

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(StartRecoverPasswordParams state)
        {
            var logger = StartEnumServer.Instance.GetLogger();

            logger.LogInfo<StartRecoverPasswordStep>($"Старт восстановления пароля для пользователя {state.User.UserName} с IP: {state.ClientIp}");

            var random = GetRandom();

            var number = Math.Abs(random).ToString(CultureInfo.InvariantCulture);

            var utc = DateTime.UtcNow;

            var item = new RecoveryLogin
            {
                RecoveryLoginID = Guid.NewGuid(),
                IsRecovered = false,
                LoginName = state.User.UserName,
                RecoveryClientIdentifier = state.ClientIp,
                RecoveryEmail = state.User.Email,
                SentNumber = number,
                UTCEventDate = utc.Date,
                UTCEventDateTime = utc
            };

            var repository = StartEnumServer.Instance.GetRepository<IRecoveryLoginRepository>();

            using (var trans = repository.GetTransaction())
            {
                trans.Begin();

                await repository.SaveNew(item);
                var url = string.Format(RecoveryUrlFormat, state.User.UserName, number);
                var body = string.Format(RecoveryMessageBodyFormat, state.User.FirstName, url);

                StartEnumServer.Instance.GetMailingService().Send(state.User.Email, RecoveryMessageTitleFormat, body);
                trans.Commit();
            }

            state.Response = new StartRecoverPasswordResponse
            {
                UserName = state.User.UserName
            };

            logger.LogInfo<StartRecoverPasswordStep>($"Письмо для подверждения смены пароля успешно отправлено {state.User.UserName}");

            return Success();
        }

        public int GetRandom()
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                return BitConverter.ToInt32(rno, 0);
            }
        }
    }
}
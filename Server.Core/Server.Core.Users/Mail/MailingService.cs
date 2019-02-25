using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Server.Core.Common;
using Server.Core.Common.Settings.Mail;
using Server.Users.Mail;

namespace Server.Core.Users.Mail
{

    /// <summary>
    /// Реализация сервиса для отправки email.
    /// </summary>
    public class MailingService: IMailingService
    {
        /// <summary>
        /// Отправлет сообщение на указаный адрес.
        /// </summary>
        /// <param name="recipient">Получатель или получатели.</param>
        /// <param name="title">Заголовок.</param>
        /// <param name="body">Тело сообщения.</param>
        /// <param name="attachments">Список вложений.</param>
        public void Send(string recipient, string title, string body, IList<KeyValuePair<string, byte[]>> attachments)
        {
            var logger = StartEnumServer.Instance.GetLogger();
            try
            {
                var settings = StartEnumServer.Instance.GetSettings<IMailingServiceSettings>();

                logger.LogInfo<MailingService>($"Старт отправки email сообщения \"{title}\" для \"{recipient}\" ");
                var smtpClient = new SmtpClient(settings.Host,
                    settings.Port);
                smtpClient.EnableSsl = settings.UseSsl;
                smtpClient.Credentials = new NetworkCredential(settings.Login,settings.Password);
                var message = new MailMessage();

                message.IsBodyHtml = false;
                //message.BodyEncoding = Encoding.Default;

                message.To.Add(recipient);

                var messageFrom = new MailAddress(settings.Login);
                message.From = messageFrom;

                message.Body = body;
                message.Subject = title;

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        var attach = AttachmentHelper.CreateAttachment(new MemoryStream(attachment.Value), attachment.Key,
                            TransferEncoding.Base64);
                        message.Attachments.Add(attach);
                    } //foreach

                }

                smtpClient.Send(message);
                message.Dispose();
                smtpClient.Dispose();
                logger.LogInfo<MailingService>(
                    $"Сообщение  \"{title}\" для \"{recipient}\" успешно отправлено.");
            }
            catch (SmtpException ex)
            {
                string inner = string.Empty;
                if (ex.InnerException != null)
                {
                    inner = ex.InnerException.Message;
                }
                logger.LogError<MailingService>(
                    $"Во время отправки1 сообщения {body} \"{title}\" для \"{recipient}\" произошла ошибка {ex.GetType()} {ex.Message} |{inner} |{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Отправлет сообщение на указаный адрес.
        /// </summary>
        /// <param name="recipient">Получатель или получатели.</param>
        /// <param name="title">Заголовок.</param>
        /// <param name="body">Тело сообщения.</param>
        /// <param name="attachments">Путь к файлам вложения.</param>
        public void Send(string recipient, string title, string body, IEnumerable<string> attachments)
        {
            var attachContents = new List<KeyValuePair<string, byte[]>>();
            foreach (string attachment in attachments)
            {
                if (File.Exists(attachment))
                {
                    attachContents.Add(new KeyValuePair<string, byte[]>(Path.GetFileName(attachment),
                        File.ReadAllBytes(attachment)));
                } //if
            } //foreach

            Send(recipient, title, body, attachContents);
        }

        /// <summary>
        /// Отправлет сообщение на указаный адрес.
        /// </summary>
        /// <param name="recipient">Получатель или получатели.</param>
        /// <param name="title">Заголовок.</param>
        /// <param name="body">Тело сообщения.</param>
        /// <param name="attachment">Путь к файлу вложения.</param>
        public void Send(string recipient, string title, string body, string attachment)
        {
            Send(recipient, title, body, new[] { attachment });
        }

        /// <summary>
        /// Отправлет сообщение на указаный адрес.
        /// </summary>
        /// <param name="recipient">Получатель или получатели.</param>
        /// <param name="title">Заголовок.</param>
        /// <param name="body">Тело сообщения.</param>
        public void Send(string recipient, string title, string body)
        {
            Send(recipient, title, body, string.Empty);
        }

    }
}

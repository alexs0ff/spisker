using Autofac;
using Microsoft.AspNetCore.Identity;
using Server.Core.Common.Entities.Users;
using Server.Core.Common.Recaptcha;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Settings.Mail;
using Server.Core.Common.Settings.Recaptcha;
using Server.Core.Users.Auth;
using Server.Core.Users.Mail;
using Server.Core.Users.Recaptcha;
using Server.Core.Users.Repositories;

namespace Server.Core.Users
{
    /// <summary>
    /// Модуль регистрации 
    /// </summary>
    public class UsersDiModule:Module
    {
        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PortalUserRepository>().As<IPortalUserRepository>();
            builder.RegisterType<MailingServiceSettings>().As<IMailingServiceSettings>();
            builder.RegisterType<MailingService>().As<IMailingService>();
            builder.RegisterType<RecoveryLoginRepository>().As<IRecoveryLoginRepository>();
            builder.RegisterType<RecaptchaSettings>().As<IRecaptchaSettings>();
            builder.RegisterType<RecaptchaService>().As<IRecaptchaService>();
        }
    }
}

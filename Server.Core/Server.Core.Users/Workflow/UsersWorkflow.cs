using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckRecaptcha;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Users.Workflow.ChangeStatusText;
using Server.Core.Users.Workflow.GetAccountSettings;
using Server.Core.Users.Workflow.UpdateAccountAvatar;
using Server.Core.Users.Workflow.UpdateAccountSettings;

namespace Server.Core.Users.Workflow
{
    /// <summary>
    /// Рабочий процесс для операция с пользователями.
    /// </summary>
    public class UsersWorkflow
    {
        public WorkflowArea Create()
        {
            var workflow = new WorkflowArea();

            workflow.RegisterStep<CheckRecaptchaStep, CheckRecaptchaParams>();
            workflow.RegisterStep<CheckUserExistsStep, CheckUserExistsParams>();
            workflow.RegisterStep<UserNotFoundStep, UserNotFoundParams>();
            workflow.RegisterStep<GetAccountSettingsStep, GetAccountSettingsParams>();
            workflow.RegisterStep<UpdateAccountSettingsStep, UpdateAccountSettingsParams>();
            workflow.RegisterStep<ChangeStatusTextStep, ChangeStatusTextParams>();
            workflow.RegisterStep<UpdateAccountAvatarStep, UpdateAccountAvatarParams>();

            return workflow;
        }
    }
}

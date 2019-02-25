using Server.Core.Common.Workflow;
using Server.Core.RestApi.Workflow.Account.ChangePassword;
using Server.Core.RestApi.Workflow.Account.Register;
using Server.Core.RestApi.Workflow.Account.StartRecoverPassword;
using Server.Core.RestApi.Workflow.RecoverPassword;
using Server.Core.Users.Workflow;

namespace Server.Core.RestApi.Workflow.Account
{
    public class AccountWorkflow
    {
        public WorkflowArea Create()
        {
            var workflow = new WorkflowArea();

            workflow.RegisterStep<AccountRegisterStep, AccountRegisterParams>();
            workflow.RegisterStep<ChangePasswordStep, ChangePasswordParams>();
            workflow.RegisterStep<StartRecoverPasswordStep, StartRecoverPasswordParams>();
            workflow.RegisterStep<RecoverPasswordStep, RecoverPasswordParams>();


            var usersWorkFlow = new UsersWorkflow();
            workflow.CopyFrom(usersWorkFlow.Create());


            return workflow;
        }
    }
}
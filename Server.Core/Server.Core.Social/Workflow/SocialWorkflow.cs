using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckListExists;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Social.Workflow.FindUsers;
using Server.Core.Social.Workflow.GetFollowings;
using Server.Core.Social.Workflow.GetProfile;
using Server.Core.Social.Workflow.GetWhoFollow;
using Server.Core.Social.Workflow.RepostList;
using Server.Core.Social.Workflow.SetLikeList;
using Server.Core.Social.Workflow.StartFollowing;
using Server.Core.Social.Workflow.StopFollowing;
using Server.Core.Social.Workflow.UnsetLikeList;

namespace Server.Core.Social.Workflow
{
    /// <summary>
    /// Рабочий процесс для социальных операций.
    /// </summary>
    public class SocialWorkflow
    {
        public WorkflowArea Create()
        {
            var workflow = new WorkflowArea();

            workflow.RegisterStep<CheckUserExistsStep, CheckUserExistsParams>();
            workflow.RegisterStep<UserNotFoundStep, UserNotFoundParams>();
            workflow.RegisterStep<StartFollowingStep, StartFollowingParams>();
            workflow.RegisterStep<StopFollowingStep, StopFollowingParams>();
            workflow.RegisterStep<GetProfileStep, GetProfileParams>();
            workflow.RegisterStep<SetLikeListStep, SetLikeListParams>();
            workflow.RegisterStep<UnsetLikeListStep, UnsetLikeListParams>();
            workflow.RegisterStep<CheckListExistsStep, CheckListExistsParams>();
            workflow.RegisterStep<RepostListStep, RepostListParams>();
            workflow.RegisterStep<FindUsersStep, FindUsersParams>();
            workflow.RegisterStep<GetWhoFollowStep, GetWhoFollowParams>();
            workflow.RegisterStep<GetFollowingsStep, GetFollowingsParams>();

            return workflow;
        }
    }
}

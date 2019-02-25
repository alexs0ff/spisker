using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckListExists;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Common.Workflow.CheckUserListExists;
using Server.Core.Lists.Workflow.AddNewList;
using Server.Core.Lists.Workflow.AddNewListItem;
using Server.Core.Lists.Workflow.CheckListItemExists;
using Server.Core.Lists.Workflow.GetUserFeed;
using Server.Core.Lists.Workflow.GetUserLists;
using Server.Core.Lists.Workflow.MoveToListItem;
using Server.Core.Lists.Workflow.Params;
using Server.Core.Lists.Workflow.RemoveList;
using Server.Core.Lists.Workflow.RemoveListItem;
using Server.Core.Lists.Workflow.Steps;
using Server.Core.Lists.Workflow.UpdateList;
using Server.Core.Lists.Workflow.UpdateListCheckItemKind;
using Server.Core.Lists.Workflow.UpdateListItem;
using Server.Core.Lists.Workflow.UpdateListItemCheck;
using Server.Core.Lists.Workflow.UpdateListKind;
using Server.Core.Lists.Workflow.UpdatePublished;

namespace Server.Core.Lists.Workflow
{
    /// <summary>
    /// Процесс работы с данными по спискам.
    /// </summary>
    public class ListWorkflow
    {
        public WorkflowArea Create()
        {
            var workflow = new WorkflowArea();

            workflow.RegisterStep<AuthListReadStep,AuthParams>();
            workflow.RegisterStep<CheckUserExistsStep, CheckUserExistsParams>();
            workflow.RegisterStep<UserNotFoundStep, UserNotFoundParams>();
            workflow.RegisterStep<FetchUserListsStep, FetchUserListsParams>();
            workflow.RegisterStep<AddNewListStep, AddNewListParams>();
            workflow.RegisterStep<ListNotFoundStep, ListNotFoundParams>();
            workflow.RegisterStep<CheckListExistsStep, CheckListExistsParams>();
            workflow.RegisterStep<UpdateListStep, UpdateListParams>();
            workflow.RegisterStep<RemoveListStep, RemoveListParams>();
            workflow.RegisterStep<AddNewListItemStep, AddNewListItemParams>();
            workflow.RegisterStep<ListItemNotFoundStep, ListItemNotFoundParams>();
            workflow.RegisterStep<UpdateListItemStep, UpdateListItemParams>();
            workflow.RegisterStep<CheckListItemExistsStep, CheckListItemExistsParams>();
            workflow.RegisterStep<RemoveListItemStep, RemoveListItemParams>();
            workflow.RegisterStep<MoveToListItemStep, MoveToListItemParams>();
            workflow.RegisterStep<GetUserFeedStep, GetUserFeedParams>();
            workflow.RegisterStep<UpdateListKindStep, UpdateListKindParams>();
            workflow.RegisterStep<UserListNotFoundStep, UserListNotFoundParams>();
            workflow.RegisterStep<CheckUserListExistsStep, CheckUserListExistsParams>();
            workflow.RegisterStep<UpdateListCheckItemKindStep, UpdateListCheckItemKindParams>();
            workflow.RegisterStep<UpdateListItemCheckStep, UpdateListItemCheckParams>();
            workflow.RegisterStep<UpdatePublishedStep, UpdatePublishedParams>();


            return workflow;
        }
    }
}

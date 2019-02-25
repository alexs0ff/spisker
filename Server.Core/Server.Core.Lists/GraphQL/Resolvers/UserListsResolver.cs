using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Server.Core.Common.GraphQL;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Lists.Workflow;
using Server.Core.Lists.Workflow.GetUserLists;

namespace Server.Core.Lists.GraphQL.Resolvers
{
    /// <summary>
    /// Резолвер для ответов по пользовательским спискам.
    /// </summary>
    public class UserListsResolver : WorkflowResolverBase<object>, IListResolver
    {
        /// <summary>
        /// Переопределение для создания workflow.
        /// </summary>
        /// <returns>Рабочий процесс.</returns>
        protected override WorkflowArea CreateWorkflow()
        {
            var workflow = new ListWorkflow();
            return workflow.Create();
        }

        /// <summary>
        /// Производит резолвинг данных с текущим контекстом.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <returns>Данные.</returns>
        public override async Task<object> Resolve(ResolveFieldContext<object> context)
        {
            var request = new GetUserListsRequest();

            request.UserName = context.GetArgument<string>("userName");
            request.LastListId = context.GetArgument<string>("lastListId");
            request.SelectedListNumber = context.GetArgument<string>("selectedListNumber");
            request.ForUserName = context.UserContext.As<UserContext>().UserName;
            request.ForUserId = context.UserContext.As<UserContext>().UserId;

            MessageOutputBase outputMessage = null;

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<FetchUserListsStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>((notFound) =>
                        {
                            outputMessage = notFound.Response;
                        })
                        .When<FetchUserListsStep, FetchUserListsParams>((fetch) =>
                        {
                            outputMessage = fetch.ListsResponse;
                        });

                }, request,
                context);

            ProcessErrors(context, outputMessage);

            return outputMessage;

        }
    }
}

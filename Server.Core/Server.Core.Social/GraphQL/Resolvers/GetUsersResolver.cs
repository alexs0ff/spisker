using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Server.Core.Common.GraphQL;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;
using Server.Core.Social.Workflow;
using Server.Core.Social.Workflow.FindUsers;

namespace Server.Core.Social.GraphQL.Resolvers
{
    /// <summary>
    /// Резолвер для поиска пользователей.
    /// </summary>
    public class GetUsersResolver : WorkflowResolverBase<object>, IGetUsersResolver
    {
        /// <summary>
        /// Переопределение для создания workflow.
        /// </summary>
        /// <returns>Рабочий процесс.</returns>
        protected override WorkflowArea CreateWorkflow()
        {
            var workflow = new SocialWorkflow();

            return workflow.Create();
        }

        /// <summary>
        /// Производит резолвинг данных с текущим контекстом.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <returns>Данные.</returns>
        public override async Task<object> Resolve(ResolveFieldContext<object> context)
        {
            var request = new FindUsersRequest();

            request.Name = context.GetArgument<string>("name");
            request.CurrentUserName = context.UserContext.As<UserContext>().UserName;

            MessageOutputBase outputMessage = null;

            object result = null;

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<FindUsersStep>();


                    flow.When<FindUsersStep, FindUsersParams>(fetch =>
                    {
                        outputMessage = fetch.Response;
                        result = fetch.Response.Profiles;
                    });

                }, request,
                context);

            ProcessErrors(context, outputMessage);

            return result;
        }
    }
}

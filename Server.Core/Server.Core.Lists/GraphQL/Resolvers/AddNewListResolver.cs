using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Server.Core.Common.GraphQL;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Lists.Workflow;
using Server.Core.Lists.Workflow.AddNewList;

namespace Server.Core.Lists.GraphQL.Resolvers
{
    /// <summary>
    /// Резолвер для добавления нового списка.
    /// </summary>
    public class AddNewListResolver : WorkflowResolverBase<object>, IAddNewListResolver
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
            var input = context.GetArgument<AddNewListRequest>("list");
            input.UserName = context.UserContext.As<UserContext>().UserName;

            MessageOutputBase outputMessage = null;

            object result = null;

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<AddNewListStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>((notFound) =>
                        {
                            outputMessage = notFound.Response;
                        })
                        .When<AddNewListStep, AddNewListParams>((add) =>
                        {
                            outputMessage = add.Response;
                            result = add.Response.List;
                        });

                }, input,
                context);

            ProcessErrors(context, outputMessage);

            return result;
        }
    }
}

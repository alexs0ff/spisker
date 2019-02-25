using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Http;

using System.Linq;
using System.Web;
using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using Microsoft.AspNetCore.Mvc;
using Server.Core.Common.GraphQL;
using Server.Core.Common.GraphQL.Json;
using Server.Core.Common;
using Server.Core.RestApi.Controllers;
using Server.Core.RestApi.Models.GraphQL;

namespace Server.RestApi.Controllers
{
    /// <summary>
    /// Контроллер для запросов GraphQL.
    /// </summary>
    public class GraphQLController: WebApiBaseController
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;
        private readonly Dictionary<string, string> _namedQueries = new Dictionary<string, string>();

        public GraphQLController()
        {
            _schema = StartEnumServer.Instance.Resolve<ISchema>();
            _executer = StartEnumServer.Instance.Resolve<IDocumentExecuter>();
            _writer = StartEnumServer.Instance.Resolve<IDocumentWriter>();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("graphql")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(HttpRequestMessage request, [FromBody]GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();
            var queryToExecute = query.Query;

            if (!string.IsNullOrWhiteSpace(query.NamedQuery))
            {
                queryToExecute = _namedQueries[query.NamedQuery];
            }
            var context = GetContext();

            var resultRaw = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = queryToExecute;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;

                _.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
                _.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
                _.UserContext = context;

            }).ConfigureAwait(false);

            var result = new ExecutionResultProxy(resultRaw);

            var httpResult = result.Errors?.Count > 0
                ? HttpStatusCode.BadRequest
                : HttpStatusCode.OK;

            var json = _writer.Write(result);

            return new ContentResult
            {
                Content = json,
                StatusCode = (int)httpResult
            };
        }

        /// <summary>
        /// Получает текущий контекст.
        /// </summary>
        /// <returns>Контекст исполнения.</returns>
        private UserContext GetContext()
        {
            return new UserContext(
                GetClientIp(),
                IsAuthenticated(),
                GetCurrentUser(),
                GetCurrentUserId(),
                GetClaims()
                );
        }
    }
}
using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Language.AST;
using Newtonsoft.Json;

namespace Server.Core.Common.GraphQL.Json
{
    /// <summary>
    /// Прокси класс для сериализации типа GraphQL.ExecutionResult
    /// </summary>
    [JsonConverter(typeof(ExecutionResultJsonConverter))]
    public class ExecutionResultProxy
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ExecutionResultProxy(ExecutionResult result)
        {
            Data = result.Data;
            Errors = result.Errors;
            Document = result.Document;
            Operation = result.Operation;
            Perf = result.Perf;
            ExposeExceptions = result.ExposeExceptions;
        }

        public object Data { get; set; }

        public ExecutionErrors Errors { get; set; }

        public string Query { get; set; }

        public Document Document { get; set; }

        public Operation Operation { get; set; }

        public PerfRecord[] Perf { get; set; }

        public bool ExposeExceptions { get; set; }
    }
}

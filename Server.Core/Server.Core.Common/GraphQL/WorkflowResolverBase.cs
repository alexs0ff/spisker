using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Server.Core.Common.Extentions;
using Server.Core.Common.Logger;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.Common.GraphQL
{
    /// <summary>
    /// Базовый класс для резолверов с рабочим процессом.
    /// </summary>
    public abstract class WorkflowResolverBase<TSourceType>:ITypeResolver<TSourceType>
    {
        /// <summary>
        /// Переопределение для создания workflow.
        /// </summary>
        /// <returns>Рабочий процесс.</returns>
        protected abstract WorkflowArea CreateWorkflow();

        /// <summary>
        /// Производит резолвинг данных с текущим контекстом.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <returns>Данные.</returns>
        public abstract Task<object> Resolve(ResolveFieldContext<TSourceType> context);

        /// <summary>
        /// Производит конфигурацию метода и вызов рабочего потока.
        /// </summary>
        /// <typeparam name="TParameters">Тип параметров.</typeparam>
        /// <param name="cfg">Объект рабочего потока для конфигурации вызово..</param>
        /// <param name="parameters">Первоначальные параметры.</param>
        /// <param name="context">Контекст выполнения.</param>
        /// <returns>Результат.</returns>
        protected async Task<WorkflowResult> Execute<TParameters>(Action<WorkflowArea> cfg, TParameters parameters, ResolveFieldContext<TSourceType> context)

        {
            WorkflowResult result = null;
            try
            {
                var workFlow = CreateWorkflow();

                cfg(workFlow);

                result = await workFlow.Start(parameters);
            }
            catch (Exception e)
            {
                var logger = StartEnumServer.Instance.GetLogger();
                var errorParameters = new List<LoggerParameter>();

                errorParameters.Add(new LoggerParameter("IsGraphQL", "true"));

                var userContext = context.UserContext.As<UserContext>();

                errorParameters.Add(new LoggerParameter("UserName", userContext.UserName));
                errorParameters.Add(new LoggerParameter("UserId", userContext.UserId ?? Guid.Empty));
                errorParameters.Add(new LoggerParameter("RequestMethod", context.Document.OriginalQuery));
                errorParameters.Add(new LoggerParameter("RequestUri", "GraphQL"));
                errorParameters.Add(new LoggerParameter("RequestUri", userContext.Ip));

                logger.LogError(GetType(), "Во время запроса GraphQL произошла ошибка: " + e.GetExceptionDetails()+ " " + userContext.Ip + "; " + context.Document.OriginalQuery, errorParameters.ToArray());

                context.Errors.Add(
                    CreateValidationError(context, (int) CommonErrors.SystemError, "системная ошибка", e));
            }

            return result;
        }

        /// <summary>
        /// Создает ошибку валидации по контексту.
        /// </summary>
        /// <param name="context">Текущий контекст.</param>
        /// <param name="errorCode">Ошибка.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="exception">Исключение.</param>
        /// <returns>Созданная ошибка</returns>
        protected ValidationError CreateValidationError(ResolveFieldContext<TSourceType> context, int errorCode,
            string message, Exception exception)
        {
            return new ValidationError(context.Document.OriginalQuery, errorCode.ToString(CultureInfo.InvariantCulture),
                message, exception, context.FieldAst);
        }

        /// <summary>
        /// Создает ошибку валидации по контексту.
        /// </summary>
        /// <param name="context">Текущий контекст.</param>
        /// <param name="errorCode">Ошибка.</param>
        /// <param name="message">Сообщение.</param>
        /// <returns>Созданная ошибка</returns>
        protected ValidationError CreateValidationError(ResolveFieldContext<TSourceType> context, int errorCode,
            string message)
        {
            return new ValidationError(context.Document.OriginalQuery, errorCode.ToString(CultureInfo.InvariantCulture),
                message, context.FieldAst);
        }

        /// <summary>
        /// Обработка ошибок сообщения.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="message">Сообщение.</param>
        protected void ProcessErrors(ResolveFieldContext<TSourceType> context, MessageOutputBase message)
        {
            if (message==null)
            {
                return;
            }
            foreach (var messageError in message.Errors)
            {
                context.Errors.Add(CreateValidationError(context, messageError.Code, messageError.Text));
            }
            
        }
    }
}

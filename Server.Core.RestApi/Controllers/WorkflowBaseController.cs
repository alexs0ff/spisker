using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Extentions;
using Server.Core.Common.Logger;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;

namespace Server.Core.RestApi.Controllers
{
    /// <summary>
    /// Базовый контроллер для поддержки workflow операций.
    /// </summary>
    public abstract class WorkflowBaseController : WebApiBaseController
    {
        

        /// <summary>
        /// Создает сообщение с ошибкой при возникновении исключения.
        /// </summary>
        /// <param name="ex">Возникшее исключение.</param>
        /// <returns>Сообщение с ошибкой.</returns>
        protected virtual MessageOutputBase CreateErrorMessage(Exception ex)
        {
            var error = new MessageOutputBase();
            error.Errors.Add(new ErrorInfo(CommonErrors.SystemError, "Произошла ошибка системы"));
            return error;
        }

        /// <summary>
        /// Переопределение для создания workflow.
        /// </summary>
        /// <returns>Рабочий процесс.</returns>
        protected abstract WorkflowArea CreateWorkflow();

        /// <summary>
        /// Производит конфигурацию метода и вызов рабочего потока.
        /// </summary>
        /// <typeparam name="TParameters">Тип параметров.</typeparam>
        /// <param name="cfg">Объект рабочего потока для конфигурации вызово..</param>
        /// <param name="parameters">Первоначальные параметры.</param>
        /// <param name="errorHandler">Хэндлер с ошибкой.</param>
        /// <returns>Результат.</returns>
        protected async Task<WorkflowResult> Execute<TParameters>(Action<WorkflowArea> cfg, TParameters parameters, Action<MessageOutputBase> errorHandler)

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

                errorParameters.Add(new LoggerParameter("IsController","true"));
                errorParameters.Add(new LoggerParameter("UserName",GetCurrentUser()));
                errorParameters.Add(new LoggerParameter("UserId", GetCurrentUserId()??Guid.Empty));
                errorParameters.Add(new LoggerParameter("RequestMethod", Request.Method));
                errorParameters.Add(new LoggerParameter("RequestUri", Request.QueryString));
                errorParameters.Add(new LoggerParameter("RequestUri", GetClientIp()));

                logger.LogError(GetType(),"Во время запроса WebApi произошла ошибка: " +e.GetExceptionDetails(), errorParameters.ToArray());
                errorHandler?.Invoke(CreateErrorMessage(e));
            }

            return result;
        }

        
    }
}
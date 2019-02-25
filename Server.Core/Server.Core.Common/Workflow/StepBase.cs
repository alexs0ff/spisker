using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Common.Workflow
{
    /// <summary>
    /// Базовый шаг
    /// </summary>
    public abstract class StepBase<TParameters>:IStepBase
        where TParameters: StepParameters,new ()
    {
        /// <summary>
        /// Возвращает тип параметров.
        /// </summary>
        /// <returns></returns>
        public Type GetParametersType()
        {
            return typeof(TParameters);
        }

        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public abstract Task<StepResult> Execute(TParameters state);

        /// <summary>
        /// Переход к следующему шагу.
        /// </summary>
        /// <returns></returns>
        protected StepResult Success()
        {
            return new StepResult(StepDirection.Next);
        }

        /// <summary>
        /// Переход к последнему шагу.
        /// </summary>
        /// <returns></returns>
        protected StepResult Finish()
        {
            return new StepResult(StepDirection.Finish);
        }

        /// <summary>
        /// Переход к определенному шагу.
        /// </summary>
        /// <returns>Результат.</returns>
        protected StepResult ToStep<TStep>()
            where TStep: StepBase<TParameters>
        {
            return new StepResult(typeof(TStep), false);
        }

        /// <summary>
        /// Переход к определенному шагу и завершение работы.
        /// </summary>
        /// <returns>Результат.</returns>
        protected StepResult ToFinish<TStep>()
            where TStep : IStepBase
        {
            return new StepResult(typeof(TStep), true);
        }
    }
}

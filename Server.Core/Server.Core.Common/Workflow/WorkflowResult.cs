using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Common.Workflow
{
    /// <summary>
    /// Результат выполнения процесса.
    /// </summary>
    public class WorkflowResult
    {
        /// <summary>
        /// Получаение последних параметров.
        /// </summary>
        /// <typeparam name="TParameters">Тип желаемых параметров.</typeparam>
        /// <returns>Результат.</returns>
        public TParameters GetStepParameters<TParameters>()
            where TParameters : StepParameters
        {
            return (TParameters)Parameters;
        }

        /// <summary>
        /// Результат выполнения.
        /// </summary>
        internal StepParameters Parameters { get; set; }

        /// <summary>
        /// Последняя выполненная операция.
        /// </summary>
        public Type LastOperation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Common.Workflow
{
    /// <summary>
    /// Результат исполнения шага.
    /// </summary>
    public class StepResult
    {
        internal Type ToStep { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal StepResult(StepDirection direction)
        {
            Direction = direction;
        }

        internal StepResult(Type nextStep, bool finish)
        {

            Direction = finish ? StepDirection.ToStepAndFinish : StepDirection.ToStep;

            ToStep = nextStep;
        }

        /// <summary>
        /// Направление шага.
        /// </summary>
        public StepDirection Direction { get; }

        /// <summary>
        /// Последние параметры.
        /// </summary>
        internal StepParameters LastParameters { get; set; }

    }
}

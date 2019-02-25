using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Common.Workflow
{
    /// <summary>
    /// Направление шага.
    /// </summary>
    public enum StepDirection
    {
        Next,

        Finish,

        ToStep,

        ToStepAndFinish
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Common.Workflow
{
    /// <summary>
    /// Базовый нтерфейс для шагов.
    /// </summary>
    public interface IStepBase
    {
        /// <summary>
        /// Возвращает тип параметров.
        /// </summary>
        /// <returns></returns>
        Type GetParametersType();
    }
}

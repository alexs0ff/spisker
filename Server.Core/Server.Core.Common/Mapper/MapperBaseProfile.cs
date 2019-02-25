using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Server.Core.Common.Workflow;

namespace Server.Core.Common.Mapper
{
    /// <summary>
    /// Базовый класс для профилей automapper.
    /// </summary>
    public class MapperBaseProfile:Profile
    {
        /// <summary>
        /// Производит регистрацию параметров для рабочего процесса.
        /// </summary>
        /// <typeparam name="TBinder"></typeparam>
        protected void RegisterWorkflowParams<TBinder>()
            where TBinder : StepParametersBinder, new()
        {
            var binder = new TBinder();
            binder.Configure(this);
        }
    }
}

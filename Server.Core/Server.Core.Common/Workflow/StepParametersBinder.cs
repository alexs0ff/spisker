using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Server.Core.Common.Workflow
{
    /// <summary>
    /// Базовый класс для конфигурирования параметров.
    /// </summary>
    public abstract class StepParametersBinder
    {
        /// <summary>
        /// Должен переопределяться для правил биндинга параметров.
        /// </summary>
        /// <param name="mapperProfile">Профиль для регистрации.</param>
        public abstract void Configure(Profile mapperProfile);
    }
}

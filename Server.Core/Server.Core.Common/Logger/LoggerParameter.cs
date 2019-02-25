using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Common.Logger
{
    /// <summary>
    /// Параметр для логгера.
    /// </summary>
    public class LoggerParameter
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public LoggerParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public LoggerParameter()
        {
        }

        /// <summary>
        /// Наименование параметра.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public object Value { get; set; }
    }
}

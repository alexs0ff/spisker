using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Common.Workflow
{
    /// <summary>
    /// Регистратор для цепочки вызовов.
    /// </summary>
    public class FlowRegistrator
    {
        /// <summary>
        /// Здесь содержаться шаги.
        /// </summary>
        private readonly LinkedList<Type> _steps = new LinkedList<Type>();

        private LinkedListNode<Type> _current;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal FlowRegistrator()
        {
            
        }

        /// <summary>
        /// Перемещает указатель на первый шаг.
        /// </summary>
        internal Type MoveFirst()
        {
            _current = _steps.First;

            return _current.Value;
        }

        /// <summary>
        /// Перемещает указатель на определенный шаг.
        /// </summary>
        internal void MoveTo(Type stepKind)
        {
            if (!_steps.Any(i=>i.Equals(stepKind)))
            {
                throw new Exception($"Операция не найдена {stepKind}");
            }
            _current = _steps.Find(stepKind);
        }

        /// <summary>
        /// Признак пустых шагов.
        /// </summary>
        /// <returns></returns>
        internal bool IsEmpty()
        {
            return !_steps.Any();
        }
        
        /// <typeparam name="TStep">Тип шага.</typeparam>
        /// <returns></returns>
        public FlowRegistrator Add<TStep>()
            where TStep:IStepBase 
        {
            _steps.AddLast(typeof(TStep));

            return this;
        }

        internal Type GetNext()
        {
            var result = _current.Value;

            _current = _current.Next;

            return result;
        }

        internal bool IsFinish()
        {
            return _current == _steps.Last;
        }

    }
}

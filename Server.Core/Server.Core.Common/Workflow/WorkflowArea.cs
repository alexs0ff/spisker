using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Server.Core.Common.Workflow
{
    /// <summary>
    /// Среда регистрации и исполнения операций.
    /// </summary>
    public sealed class WorkflowArea
    {
        private IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WorkflowArea()
        {
            _mapper = StartEnumServer.Instance.GetMapper();
        }

        /// <summary>
        /// Зарегистрированные шаги.
        /// </summary>
        private FlowRegistrator _registrator;

        /// <summary>
        /// Страт регистрации шагов.
        /// </summary>
        /// <returns></returns>
        public FlowRegistrator StartRegisterFlow()
        {
            _registrator = new FlowRegistrator();

            return _registrator;
        }

        /// <summary>
        /// Здесь содержаться шаги.
        /// </summary>
        private readonly IDictionary<Type, IStepBase> _steps = new Dictionary<Type, IStepBase>();

        /// <summary>
        /// Регистрация шага для вызова.
        /// </summary>
        public WorkflowArea RegisterStep<TStep, TParameters>()
            where TStep: StepBase<TParameters>,new()
            where TParameters : StepParameters, new()
        {
            _steps[typeof(TStep)] = new TStep();

            return this;
        }

        /// <summary>
        /// Копирует шаги из ддругого рабочего процесса.
        /// </summary>
        /// <param name="area">Рабочий процесс для копирования шагов.</param>
        public void CopyFrom(WorkflowArea area)
        {
            foreach (var areaStep in area._steps)
            {
                _steps.Add(areaStep.Key,areaStep.Value);
            }
        }

        /// <summary>
        /// Стартует рабочий поток на испольнение.
        /// </summary>
        /// <param name="parameters">Параметры запуска первого шага.</param>
        /// <returns>Завершающий шаг.</returns>
        public async Task<WorkflowResult> Start<TParameters>(TParameters parameters)
        {
            if (_registrator == null || _registrator.IsEmpty())
            {
                throw new Exception("Необходимо инициализировать шаги");
            }


            bool isFinish = false;

            Type lastOperation = _registrator.MoveFirst();

            var step = _steps[lastOperation];

            StepParameters lastParameters =
                (StepParameters) _mapper.Map(parameters, parameters.GetType(), step.GetParametersType());

            do
            {
                var internalIsFinish = _registrator.IsFinish();
                lastOperation = _registrator.GetNext();

                var result = await ExecuteStep(lastParameters, lastOperation);

                if (result.Direction == StepDirection.Finish)
                {
                    isFinish = true;
                }
                else if (result.Direction == StepDirection.ToStep)
                {
                    _registrator.MoveTo(result.ToStep);
                }
                else if (result.Direction == StepDirection.ToStepAndFinish)
                {
                    isFinish = true;
                    lastOperation = result.ToStep;
                    result = await ExecuteStep(result.LastParameters, result.ToStep);
                }
                else if (result.Direction == StepDirection.Next)
                {
                    if (internalIsFinish)
                    {
                        isFinish = true;
                    }
                }

                lastParameters = result.LastParameters;

            } while (!isFinish);

            WorkflowResult workflow = new WorkflowResult();

            workflow.Parameters = lastParameters;
            workflow.LastOperation = lastOperation;

            Invoke(lastOperation, lastParameters);

            return workflow;
        }

        private async Task<StepResult> ExecuteStep(StepParameters lastParameters, Type operationToExec)
        {
            if (!_steps.ContainsKey(operationToExec))
            {
                throw new Exception($"Операция {operationToExec} не зарегистрированна");
            }

            var step = _steps[operationToExec];

            StepParameters newParameters;

            var stepParametersType = step.GetParametersType();
            var lastParametersType = lastParameters.GetType();

            if (lastParametersType == stepParametersType)
            {
                newParameters = lastParameters;
            }
            else
            {
                newParameters = (StepParameters)_mapper.Map(lastParameters, lastParametersType, stepParametersType);
            }

            var methodInfo = step.GetType().GetMethod("Execute");

            var methodResult =  (Task<StepResult>)methodInfo.Invoke(step,new []{ newParameters });
            var result = await methodResult;

            result.LastParameters = newParameters;

            return result;
        }

        /// <summary>
        /// Вызывает соответствующий метод с конечным результатом.
        /// </summary>
        private void Invoke(Type lastOperation, StepParameters parameters)

        {
            if (_actions.ContainsKey(lastOperation))
            {
                _actions[lastOperation].DynamicInvoke(parameters);
            }
        }

        /// <summary>
        /// События для вызова.
        /// </summary>
        private readonly IDictionary<Type, Delegate> _actions =
            new Dictionary<Type, Delegate>();

        /// <summary>
        /// Создает делегат вызова в соответсвии с последним вызывающемся методом.
        /// </summary>
        /// <typeparam name="TParameters">Тип параметров вызова.</typeparam>
        /// <typeparam name="TStep">Тип шага.</typeparam>
        /// <param name="action">Действие для вызова.</param>
        public WorkflowArea When<TStep,TParameters>(Action<TParameters> action)
            where TParameters : StepParameters
            where TStep:IStepBase
        {
            var type = typeof(TStep);
            if (_actions.ContainsKey(type))
            {
                return this;
            }
            _actions[type] = action;
            return this;
        }
    }
}

using AutoMapper;
using Server.Core.Common.Workflow.CheckUserExists;

namespace Server.Core.Common.Workflow
{
    public class CommonBinder : StepParametersBinder
    {
        /// <summary>
        /// Должен переопределяться для правил биндинга параметров.
        /// </summary>
        /// <param name="mapperProfile">Профиль для регистрации.</param>
        public override void Configure(Profile mapperProfile)
        {
            mapperProfile.CreateMap<CheckUserExistsParams, UserNotFoundParams>();
        }
    }
}

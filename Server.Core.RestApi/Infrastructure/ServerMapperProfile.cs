using System;
using Server.Core.Common.Mapper;
using Server.Core.Common.Workflow;
using Server.Core.RestApi.Workflow;

namespace Server.Core.RestApi.Infrastructure
{
    public class ServerMapperProfile: MapperBaseProfile
    {
        public ServerMapperProfile()
        {
            RegisterWorkflowParams<WorkflowBinder>();
            RegisterWorkflowParams<CommonBinder>();
        }
    }
}
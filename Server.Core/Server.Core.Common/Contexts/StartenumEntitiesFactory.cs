using System;
using System.Collections.Generic;
using System.Text;
using Server.Core.Common.Repositories;

namespace Server.Core.Common.Contexts
{
    /// <summary>
    /// Фабрика для контекста.
    /// </summary>
    public abstract class StartenumEntitiesFactory: DbContextFactory<StartenumEntities>
    {
    }
}

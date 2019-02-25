using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Server.Core.Common.Repositories
{
    /// <summary>
    /// Фабрика контекста.
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class DbContextFactory<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Создание контекста.
        /// </summary>
        /// <returns>Созданный контекст.</returns>
        public abstract TDbContext Create();
    }
}

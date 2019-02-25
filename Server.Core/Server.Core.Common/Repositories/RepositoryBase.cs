using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Repositories;

namespace Server.Core.Common.Repositories
{
    /// <summary>
    /// Базовый класс для репозиторев.
    /// </summary>
    public abstract class RepositoryBase<TDbContext,TDbContextFactory> : IRepository
        where TDbContext : DbContext
        where TDbContextFactory: DbContextFactory<TDbContext>
    {
        private TDbContext _context;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        protected RepositoryBase(TDbContextFactory factory)
        {
            _context = factory.Create();
        }

        protected TDbContext GetContext()
        {
            return _context;
        }

        /// <summary>
        /// Установка контекста из репозитория.
        /// </summary>
        /// <param name="repository">Репозиторий.</param>
        private void SetContext(RepositoryBase<TDbContext, TDbContextFactory> repository)
        {
            _context = repository.GetContext();
        }

        /// <summary>
        /// Забирает единый контекст из другого репозитория.
        /// </summary>
        /// <param name="repository">Репозиторий от куда расшарить контекст, контексты у репозиториев должны быть одинаковые</param>
        public void ShareContext(IRepository repository)
        {
            switch (repository)
            {
                case null:
                    throw new ArgumentNullException(nameof(repository));
                case RepositoryBase<TDbContext, TDbContextFactory> rep:
                    SetContext(rep);
                    break;
                default:
                    throw new InvalidOperationException($"У репозитория должен быть тип контекста {_context.GetType()}");
            }
        }

        /// <summary>
        /// Начать транзакцию в контексте.
        /// </summary>
        public RepositoryTransaction GetTransaction()
        {
            return new RepositoryTransaction(GetContext().Database);
        }
    }
}

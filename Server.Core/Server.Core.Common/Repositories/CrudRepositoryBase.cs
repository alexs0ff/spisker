using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Entities;


namespace Server.Core.Common.Repositories
{
    /// <summary>
    /// Crud репозитоий для определенной сущности.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    /// <typeparam name="TKey">Тип ключа сущности.</typeparam>
    /// <typeparam name="TDbContext">Тип контекста.</typeparam>
    /// <typeparam name="TDbContextFactory">Фабрика типа контекста.</typeparam>
    public abstract class CrudRepositoryBase<TEntity, TKey, TDbContext, TDbContextFactory> : EntityRepositoryBase<TEntity, TKey,TDbContext, TDbContextFactory>, ICrudRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : struct
        where TDbContext : DbContext
        where TDbContextFactory : DbContextFactory<TDbContext>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        protected CrudRepositoryBase(TDbContextFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Сохраняет новую сущность в базе.
        /// </summary>
        /// <param name="entity">Сохраняемая сущность.</param>
        /// <returns>Значение созданного ключа.</returns>
        public async Task<TKey> SaveNew(TEntity entity)
        {
            var context = GetContext();
            context.Set<TEntity>().Add(entity);

            await context.SaveChangesAsync();

            return entity.GetId();
        }

        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        /// <param name="entity">Сущность для обновления.</param>
        /// <returns>Task.</returns>
        public async Task Update(TEntity entity)
        {
            var context = GetContext();
            var savedEntity = await GetById(entity.GetId());

            if (savedEntity != null)
            {
                entity.CopyTo(savedEntity);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление сущности из репозитория.
        /// </summary>
        /// <param name="id">Код сущности.</param>
        public async Task Delete(TKey id)
        {
            var savedEntity = await GetById(id);

            if (savedEntity != null)
            {
                var context = GetContext();

                context.Set<TEntity>().Remove(savedEntity);
                await context.SaveChangesAsync();
            }
        }
    }
}

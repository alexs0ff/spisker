using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Server.Core.Common.Repositories
{
    /// <summary>
    /// Объект управления транзакциями.
    /// </summary>
    public class RepositoryTransaction : IDisposable
    {
        private readonly DatabaseFacade _dataBase;

        private IDbContextTransaction _transaction;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        internal RepositoryTransaction(DatabaseFacade database)
        {
            _dataBase = database;
        }

        public void Begin()
        {
            if (_transaction == null)
            {
                _transaction = _dataBase.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Rollback();
        }
    }
}

using System;
using System.Data;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        
        /// <summary>
        /// Creating new transaction for that current instance
        /// </summary>
        public void Begin();
        /// <summary>
        /// Commiting transaction of that current instance
        /// </summary>
        public void Commit();
        /// <summary>
        /// Rollback (cancelling) changes that made in that current transaction context
        /// </summary>
        public void Rollback();
    }
}
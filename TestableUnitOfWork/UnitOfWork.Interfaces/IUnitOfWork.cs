using System;
using System.Data;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        public void Begin();
        public void Commit();
        public void Rollback();
    }
}
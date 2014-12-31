using System;

namespace SqlCeOrm.DataAccess
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
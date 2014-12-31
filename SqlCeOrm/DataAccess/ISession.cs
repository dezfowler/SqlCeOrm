using System;

namespace SqlCeOrm.DataAccess
{
    public interface ISession : IDisposable
    {
        ITable OpenTable(string tableName);

        ITransaction BeginTran();
    }
}
using System.Data.SqlServerCe;

namespace SqlCeOrm.DataAccess
{
    internal class SqlCeTran : ITransaction
    {
        public SqlCeTran(SqlCeSession sqlCeSession)
        {
            InnerTransaction = sqlCeSession.Connection.BeginTransaction();
        }

        internal SqlCeTransaction InnerTransaction { get; private set; }

        public void Dispose()
        {
            if (InnerTransaction != null)
            {
                InnerTransaction.Dispose();
                InnerTransaction = null;
            }
        }

        public void Commit()
        {
            InnerTransaction.Commit();
        }

        public void Rollback()
        {
            InnerTransaction.Rollback();
        }
    }
}
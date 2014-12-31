using System;
using System.Data;
using System.Data.SqlServerCe;

namespace SqlCeOrm.DataAccess
{
    /// <summary>
    /// Manages the database connection and transaction for the data access session
    /// </summary>
    public class SqlCeSession : ISession
    {
        private readonly SqlCePersistentStore _sqlCePersistentStore;
        private SqlCeConnection _connection = null;
        private SqlCeTran _currentTransaction = null;

        public SqlCeSession(SqlCePersistentStore sqlCePersistentStore)
        {
            _sqlCePersistentStore = sqlCePersistentStore;
        }

        internal SqlCeConnection Connection
        {
            get { return _connection ?? (_connection = _sqlCePersistentStore.CreateAndOpenConnection()); }
        }

        internal SqlCeTran CurrentTransaction
        {
            get { return _currentTransaction; }
        }

        public ITransaction BeginTran()
        {
            if (CurrentTransaction != null)
            {
                throw new SqlCePersistenceException("Transaction already begun");
            }

            return (_currentTransaction = new SqlCeTran(this));
        }

        public void Dispose()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }

            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }

                _connection.Dispose();
            }
        }

        public ITable OpenTable(string tableName)
        {
            return new SqlCeTable(this, tableName, _sqlCePersistentStore.TableMeta[tableName]);
        }

        public void ApplyTransaction(SqlCeCommand command, ITransaction transaction)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (transaction == null) throw new ArgumentNullException("transaction");


            if (CurrentTransaction != transaction)
            {
                throw new SqlCePersistenceException("Transaction is not valid for this session");
            }

            command.Transaction = CurrentTransaction.InnerTransaction;
        }
    }
}
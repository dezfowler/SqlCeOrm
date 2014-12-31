using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;

namespace SqlCeOrm.DataAccess
{
    internal class SqlCeTable : SqlCeRowSet, ITable
    {
        public SqlCeTable(SqlCeSession session, string tableName, SqlCePersistentStore.TableMetaData tableMetaData)
            : base(session, tableName, tableMetaData)
        {
        }

        public IUpdatableRow NewRow(ITransaction transaction)
        {
            using (var command = new SqlCeCommand())
            {
                command.CommandType = CommandType.TableDirect;
                command.CommandText = _tableName;
                command.Connection = _session.Connection;

                _session.ApplyTransaction(command, transaction);

                var resultSet = command.ExecuteResultSet(ResultSetOptions.Updatable);
                return new SqlCeUpdatableRow(null, resultSet, null);
            }
        }
    }
}
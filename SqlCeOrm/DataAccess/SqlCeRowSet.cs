using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;

namespace SqlCeOrm.DataAccess
{
    internal class SqlCeRowSet : IRowSet
    {
        protected readonly SqlCeSession _session;
        protected readonly SqlCeCommand _command;
        protected readonly string _tableName;
        protected readonly SqlCePersistentStore.TableMetaData _tableMetaData;

        public SqlCeRowSet(SqlCeSession session, SqlCeCommand command)
        {
            _session = session;
            _command = command;
        }

        public SqlCeRowSet(SqlCeSession session, string tableName, SqlCePersistentStore.TableMetaData tableMetaData)
        {
            _session = session;
            _tableName = tableName;
            _tableMetaData = tableMetaData;

            _command = SetupCommand();
        }

        public SqlCeCommand Command
        {
            get { return _command; }
        }

        public SqlCeSession Session
        {
            get { return _session; }
        }

        public IEnumerable<IUpdatableRow> Edit(ITransaction transaction)
        {
            return new SqlCeUpdatableRowSet(this, transaction);
        }

        public IEnumerable<IRow> Read()
        {
            using (var reader = Command.ExecuteReader())
            {
                var sqlCeRow = new SqlCeRow(reader);
                while (reader.Read())
                {
                    yield return sqlCeRow;
                }
            }
        }

        public IRowSet Filter(IDictionary<string, object> criteria)
        {
            var command = SetupCommand();

            if (criteria != null && criteria.Count != 0)
            {
                command.IndexName = _tableMetaData.GetIndexNameForColumns(criteria.Keys.ToArray());
                command.SetRange(DbRangeOptions.Match, criteria.Values.ToArray(), null);
            }

            return new SqlCeRowSet(_session, command);
        }

        public IRowSet Filter(IEnumerable<string> fields, object[,] ranges)
        {
            if (fields == null) throw new ArgumentNullException("fields");
            if (ranges == null) throw new ArgumentNullException("ranges");

            var columnNames = new List<string>(fields);

            if (columnNames.Count == 0) throw new ArgumentException("Cannot have no fields defined", "fields");
            if (ranges.Rank == 2 && ranges.GetUpperBound(1) == 1) throw new ArgumentException("Must have a second dimension of upper bound 1", "ranges");
            if (columnNames.Count != ranges.GetUpperBound(0)) throw new ArgumentException("Must have same number of ranges and fields", "ranges");

            var command = SetupCommand();
            
            command.IndexName = _tableMetaData.GetIndexNameForColumns(columnNames.ToArray());
            command.SetRange(DbRangeOptions.InclusiveStart | DbRangeOptions.InclusiveEnd,
                GetDimension(ranges, 0), GetDimension(ranges, 1));
            
            return new SqlCeRowSet(_session, command);
        }

        public IRowSet OrderBy(IEnumerable<string> fields)
        {
            var command = SetupCommand();

            if (fields != null && fields.Any())
            {
                command.IndexName = _tableMetaData.GetIndexNameForColumns(fields.ToArray());
            }

            return new SqlCeRowSet(_session, command);
        }

        private SqlCeCommand SetupCommand()
        {
            return new SqlCeCommand
            {
                CommandType = CommandType.TableDirect,
                CommandText = _tableName,
                Connection = _session.Connection,
            };
        }

        private object[] GetDimension(object[,] ary, int dimension)
        {
            object[] ret = new object[ary.GetUpperBound(0) + 1];
            
            for(var i = ary.GetLowerBound(0); i <= ary.GetUpperBound(0); i++)
            {
                ret[i] = ary[i, dimension];
            }

            return ret;
        }
    }
}
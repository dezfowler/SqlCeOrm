using System;
using System.Data.SqlServerCe;

namespace SqlCeOrm.DataAccess
{
    internal class SqlCeUpdatableRow : IUpdatableRow
    {
        private readonly SqlCeUpdatableRowSet _rowSet;
        private readonly SqlCeResultSet _resultSet;
        private readonly SqlCeUpdatableRecord _record;
        private readonly bool _inserting = false;

        public SqlCeUpdatableRow(SqlCeUpdatableRowSet rowSet, SqlCeResultSet resultSet, SqlCeUpdatableRecord record)
        {
            if (resultSet == null) throw new ArgumentNullException("resultSet");
            
            if (record == null)
            {
                record = resultSet.CreateRecord();
                _inserting = true;
            }

            _rowSet = rowSet;
            _resultSet = resultSet;
            _record = record;
        }

        public object GetValue(string fieldName)
        {
            return _resultSet[fieldName];
        }

        public void SetValue(string fieldName, object value)
        {
            _record[fieldName] = value;
        }

        public void Delete()
        {
            _resultSet.Delete();
        }

        public void Save()
        {
            if (_inserting)
            {
                _resultSet.Insert(_record, DbInsertOptions.PositionOnInsertedRow);
            }
            else
            {
                _resultSet.Update();
            }
        }

        public void Dispose()
        {
            if (_rowSet != null && _rowSet.DisposeDeferred)
            {
                _rowSet.DisposeInternal();
            }
            else if (_rowSet == null)
            {
                _resultSet.Close();
            }
        }
    }
}
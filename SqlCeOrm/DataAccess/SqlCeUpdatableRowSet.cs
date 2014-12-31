using System.Collections;
using System.Collections.Generic;
using System.Data.SqlServerCe;

namespace SqlCeOrm.DataAccess
{
    internal class SqlCeUpdatableRowSet : IEnumerator<IUpdatableRow>, IEnumerable<IUpdatableRow>
    {
        private SqlCeResultSet _resultSet;
        private ResultSetEnumerator _resultSetEnumerator;
        private SqlCeUpdatableRow _current;

        public SqlCeUpdatableRowSet(SqlCeRowSet rowSet, ITransaction transaction)
        {
            var sqlCeCommand = rowSet.Command;

            rowSet.Session.ApplyTransaction(sqlCeCommand, transaction);

            _resultSet = sqlCeCommand.ExecuteResultSet(ResultSetOptions.Updatable | ResultSetOptions.Scrollable);
            _resultSetEnumerator = (ResultSetEnumerator)_resultSet.GetEnumerator();
        }

        public bool DisposeDeferred { get; private set; }

        public void Dispose()
        {
            if (_current != null)
            {
                // Not moved to the end so defer dispose
                DisposeDeferred = true;
                return;
            }

            DisposeInternal();
        }

        internal void DisposeInternal()
        {
            if (_resultSet != null)
            {
                _resultSet.Close();
                _resultSet.Dispose();
                _resultSet = null;
            }
        }

        public bool MoveNext()
        {
            if (_current != null)
            {
                _current.Dispose();
                _current = null;
            }

            return _resultSetEnumerator.MoveNext() && WrapNext();
        }

        private bool WrapNext()
        {
            _current = new SqlCeUpdatableRow(this, _resultSet, _resultSetEnumerator.Current);
            return true;
        }

        public void Reset()
        {
            _resultSetEnumerator.Reset();
        }

        public IUpdatableRow Current
        {
            get { return _current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public IEnumerator<IUpdatableRow> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
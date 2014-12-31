using System.Data.Common;

namespace SqlCeOrm.DataAccess
{
    public class SqlCeRow : IRow
    {
        private readonly DbDataReader _reader;

        public SqlCeRow(DbDataReader reader)
        {
            _reader = reader;
        }

        public object GetValue(string fieldName)
        {
            return _reader[fieldName];
        }
    }
}
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;

namespace SqlCeOrm.DataAccess
{
    public class SqlCePersistentStore : IPersistentStore
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, TableMetaData> _tableMeta = new Dictionary<string, TableMetaData>();

        public SqlCePersistentStore(string connectionString)
        {
            _connectionString = connectionString;
            CacheMetaData();
        }

        internal Dictionary<string, TableMetaData> TableMeta
        {
            get { return _tableMeta; }
        }

        public ISession BeginSession()
        {
            return new SqlCeSession(this);
        }

        internal SqlCeConnection CreateAndOpenConnection()
        {
            var conn = new SqlCeConnection(_connectionString);
            conn.Open();
            return conn;
        }

        private void CacheMetaData()
        {
            using (var conn = CreateAndOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT TABLE_NAME, INDEX_NAME, PRIMARY_KEY, COLUMN_NAME 
FROM INFORMATION_SCHEMA.INDEXES
ORDER BY TABLE_NAME, INDEX_NAME, ORDINAL_POSITION";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tableName = (string)reader["TABLE_NAME"];
                        var indexName = (string)reader["INDEX_NAME"];
                        var primaryKey = (bool)reader["PRIMARY_KEY"];
                        var columnName = (string)reader["COLUMN_NAME"];

                        TableMetaData meta;
                        if (!TableMeta.TryGetValue(tableName, out meta))
                        {
                            meta = new TableMetaData();
                            TableMeta.Add(tableName, meta);
                        }

                        if (primaryKey)
                        {
                            if (meta.PrimaryKeyIndexName == null)
                            {
                                meta.PrimaryKeyIndexName = indexName;
                            }

                            if (meta.PrimaryKeyIndexName != indexName)
                            {
                                throw new SqlCePersistenceException("Conflicting primary key index name");
                            }
                        }

                        List<string> columnNames;
                        if (!meta.Indexes.TryGetValue(indexName, out columnNames))
                        {
                            columnNames = new List<string>();
                            meta.Indexes.Add(indexName, columnNames);
                        }

                        columnNames.Add(columnName);
                    }
                    reader.Close();
                }
            }
        }

        internal class TableMetaData
        {
            public string PrimaryKeyIndexName;
            public readonly Dictionary<string, List<string>> Indexes = new Dictionary<string, List<string>>();

            public string GetIndexNameForColumns(params string[] columnNames)
            {
                foreach (var index in Indexes)
                {
                    if (index.Value.SequenceEqual(columnNames))
                    {
                        return index.Key;
                    }
                }

                throw new SqlCePersistenceException("Matching index not found");
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace SqlCeOrm.DataAccess
{
    public static class ITableExtensions
    {
        public static IRowSet All(this ITable table)
        {
            return table.Filter(null);
        }

        public static IRow ReadRow(this ITable table, int id)
        {
            return table
                .Filter(new Dictionary<string, object> { { "ID", id } })
                .Read()
                .SingleOrDefault();
        }

        public static IUpdatableRow EditRow(this ITable table, int id, ITransaction tran)
        {
            return table
                .Filter(new Dictionary<string, object> { { "ID", id } })
                .Edit(tran)
                .SingleOrDefault();
        }

        public static void DeleteRow(this ITable table, int id, ITransaction tran)
        {
            table
                .Filter(new Dictionary<string, object> { { "ID", id } })
                .Edit(tran)
                .Single()
                .Delete();
        }
    }
}
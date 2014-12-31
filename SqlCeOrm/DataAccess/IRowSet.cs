using System.Collections.Generic;

namespace SqlCeOrm.DataAccess
{
    public interface IRowSet
    {
        IEnumerable<IUpdatableRow> Edit(ITransaction transaction);

        IEnumerable<IRow> Read();

        IRowSet Filter(IDictionary<string, object> criteria);

        IRowSet OrderBy(IEnumerable<string> fields);
    }
}
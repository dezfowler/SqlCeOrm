using System.Collections.Generic;

namespace SqlCeOrm.DataAccess
{
    public interface ITable : IRowSet
    {
        IUpdatableRow NewRow(ITransaction transaction);
    }
}
using System;

namespace SqlCeOrm.DataAccess
{
    public interface IUpdatableRow : IRow, IDisposable
    {
        void SetValue(string fieldName, object value);

        void Delete();

        void Save();
    }
}
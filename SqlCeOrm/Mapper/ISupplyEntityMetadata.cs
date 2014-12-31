using System.Collections.Generic;

namespace SqlCeOrm.Mapper
{
    public interface ISupplyEntityMetadata<T>
    {
        string TableName { get; }

        IEnumerable<FieldDescriptor<T>> GetFieldDescriptors();
    }
}
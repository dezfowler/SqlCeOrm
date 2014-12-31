using System;
using System.Data;

namespace SqlCeOrm.Mapper
{
    public class FieldDescriptor<T>
    {
        public string FieldName { get; set; }

        public DbType FieldType { get; set; }

        public Func<T, object> ValueGetter { get; set; }

        public Action<T, object> ValueSetter { get; set; }
    }
}
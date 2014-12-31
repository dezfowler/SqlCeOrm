using System.Collections.Generic;
using System.Linq;
using SqlCeOrm.DataAccess;
using SqlCeOrm.Repository;

namespace SqlCeOrm.Mapper
{
    public class Mapper<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IEnumerable<FieldDescriptor<TEntity>> _fieldsToMap;

        public Mapper(ISupplyEntityMetadata<TEntity> meta)
            : this(meta, null)
        {
        }

        public Mapper(ISupplyEntityMetadata<TEntity> meta, IEnumerable<string> fieldsToMap)
        {
            _fieldsToMap = (fieldsToMap != null) ? meta.GetFieldDescriptors().Where(field => fieldsToMap.Contains(field.FieldName)) :
                                                                                                                                          meta.GetFieldDescriptors();
        }

        public void WriteToRow(TEntity entity, IUpdatableRow row)
        {
            foreach (var field in _fieldsToMap)
            {
                row.SetValue(field.FieldName, field.ValueGetter(entity));
            }
        }

        public TEntity ReadFromRow(IRow row)
        {
            var entity = new TEntity();

            foreach (var field in _fieldsToMap)
            {
                field.ValueSetter(entity, row.GetValue(field.FieldName));
            }

            return entity;
        }
    }
}
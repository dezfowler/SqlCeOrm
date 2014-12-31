using SqlCeOrm.DataAccess;
using SqlCeOrm.Mapper;

namespace SqlCeOrm.Repository
{
    /// <summary>
    /// Abstract base class for standard single table CRUD repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly IPersistentStore _store;

        protected Repository(IPersistentStore store)
        {
            _store = store;
        }

        public void Upsert(TEntity entity)
        {
            using (var session = _store.BeginSession())
            {
                using (var tran = session.BeginTran())
                {
                    var table = session.OpenTable(EntityMeta.TableName);

                    using (var row = table.EditRow(entity.ID, tran) ?? table.NewRow(tran))
                    {
                        var mapper = new Mapper<TEntity>(EntityMeta);
                        mapper.WriteToRow(entity, row);
                    }

                    tran.Commit();
                }
            }
        }

        public TEntity Read(int id)
        {
            using (var session = _store.BeginSession())
            {
                var table = session.OpenTable(EntityMeta.TableName);

                var row = table.ReadRow(id);
                if (row == null) return null;

                var mapper = new Mapper<TEntity>(EntityMeta);
                return mapper.ReadFromRow(row);
            }
        }

        public void Delete(TEntity entity)
        {
            using (var session = _store.BeginSession())
            {
                using (var tran = session.BeginTran())
                {
                    var table = session.OpenTable(EntityMeta.TableName);
                    table.DeleteRow(entity.ID, tran);

                    tran.Commit();
                }
            }
        }

        protected abstract ISupplyEntityMetadata<TEntity> EntityMeta { get; }
    }
}
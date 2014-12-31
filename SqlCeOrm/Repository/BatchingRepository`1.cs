using System;
using System.Collections.Generic;
using System.Linq;
using SqlCeOrm.DataAccess;
using SqlCeOrm.Mapper;

namespace SqlCeOrm.Repository
{
    public abstract class BatchingRepository<TEntity> : Repository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly int _batchSize;

        protected BatchingRepository(IPersistentStore store, int batchSize)
            : base(store)
        {
            _batchSize = batchSize;
        }

        public IEnumerable<TEntity> GetNextBatch()
        {
            using (var session = _store.BeginSession())
            {
                foreach (var row in session.OpenTable(EntityMeta.TableName).Read().Take(_batchSize))
                {
                    var mapper = new Mapper<TEntity>(EntityMeta);
                    yield return mapper.ReadFromRow(row);
                }
            }
        }

        public void DeleteBatch(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
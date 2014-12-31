using SqlCeOrm.DataAccess;

namespace SqlCeOrm.Repository
{
    public abstract class SigningRepository<TEntity> : Repository<TEntity> where TEntity : class, ISignable, IEntity, new()
    {
        protected SigningRepository(IPersistentStore store, ISignatureAlgorithm signatureAlgorithm)
            : base(store)
        {
        }
    }
}
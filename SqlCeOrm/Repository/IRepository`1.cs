namespace SqlCeOrm.Repository
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity, new()
    {
        void Upsert(TEntity entity);

        void Delete(TEntity entity);
    }
}
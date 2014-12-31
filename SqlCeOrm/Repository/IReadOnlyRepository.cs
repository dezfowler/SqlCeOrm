namespace SqlCeOrm.Repository
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Read(int id);

        // Some other methods allowing generic light-weight search?
    }
}
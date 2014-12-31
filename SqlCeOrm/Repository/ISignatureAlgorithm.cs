namespace SqlCeOrm.Repository
{
    public interface ISignatureAlgorithm
    {
        Signed<TEntity> Sign<TEntity>(TEntity entity) where TEntity : ISignable;

        bool Verify<TEntity>(Signed<TEntity> entity) where TEntity : ISignable;
    }
}
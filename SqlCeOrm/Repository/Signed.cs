namespace SqlCeOrm.Repository
{
    public class Signed<TEntity> where TEntity : ISignable
    {
        internal Signed(TEntity entity, byte[] signature)
        {
            Entity = entity;
            Signature = signature;
        }

        public TEntity Entity { get; private set; }

        public byte[] Signature { get; private set; }
    }
}
namespace SqlCeOrm.DataAccess
{
    /// <summary>
    /// Interface for persistent store which provides data access sessions
    /// </summary>
    public interface IPersistentStore
    {
        ISession BeginSession();
    }
}
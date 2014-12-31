using System;

namespace SqlCeOrm.DataAccess
{
    [Serializable]
    public class SqlCePersistenceException : PersistenceException
    {
        public SqlCePersistenceException()
        {
        }

        public SqlCePersistenceException(string message)
            : base(message)
        {
        }

        public SqlCePersistenceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
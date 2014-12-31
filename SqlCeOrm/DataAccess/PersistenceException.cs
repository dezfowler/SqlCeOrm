using System;

namespace SqlCeOrm.DataAccess
{
    [Serializable]
    public abstract class PersistenceException : Exception
    {
        protected PersistenceException()
        {
        }

        protected PersistenceException(string message)
            : base(message)
        {
        }

        protected PersistenceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
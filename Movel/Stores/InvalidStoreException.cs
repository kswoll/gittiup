using System;

namespace Movel.Stores
{
    public class InvalidStoreException : Exception
    {
        public InvalidStoreException(string message) : base(message)
        {
        }
    }
}
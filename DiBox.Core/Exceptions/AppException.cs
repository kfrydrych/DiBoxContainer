using System;

namespace DiBox.Core.Exceptions
{
    public abstract class AppException : Exception
    {
        public readonly Type Type;

        protected AppException(Type type)
        {
            Type = type;
        }
    }
}
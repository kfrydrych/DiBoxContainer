using System;

namespace DiBox.Core.Exceptions
{
    public class ComponentHasMultiplyConstructorsException : AppException
    {
        public ComponentHasMultiplyConstructorsException(Type type) : base(type)
        {
        }
    }
}

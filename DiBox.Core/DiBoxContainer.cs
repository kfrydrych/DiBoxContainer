using DiBox.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiBox.Core
{
    public class DiBoxContainer
    {
        private readonly Dictionary<Type, Type> _registaions = new Dictionary<Type, Type>();

        public void Register<TRegistration, TImplementation>()
        {
            _registaions.Add(typeof(TRegistration), typeof(TImplementation));
        }

        public object Resolve(Type requiredType)
        {
            var type = _registaions[requiredType];

            var constructors = type.GetConstructors();

            if (constructors.Length > 1)
                throw new ComponentHasMultiplyConstructorsException(type);

            var constructor = constructors.First();

            var dependencyTypes = constructor.GetParameters().Select(x => x.ParameterType);

            var dependencies = dependencyTypes.Select(Resolve).ToArray();

            var instance = Activator.CreateInstance(type, dependencies);

            return instance;
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
    }
}

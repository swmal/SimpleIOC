using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public class TypeActivator : ITypeActivator
    {
        private readonly IConstructorSelector _constructorSelector;
        private readonly Container _container;

        public TypeActivator(Container container, IConstructorSelector constructorSelector)
        {
            _constructorSelector = constructorSelector;
            _container = container;
        }
        public object CreateInstance(Type type)
        {
            var constructor = _constructorSelector.Select(type, _container);
            var argList = new List<object>();
            foreach (var param in constructor.GetParameters())
            {
                var reg = _container.GetRegistrationOf(param.ParameterType);
                argList.Add(reg.GetImplementationOf());
            }
            return Activator.CreateInstance(type, argList.ToArray());
        }
    }
}

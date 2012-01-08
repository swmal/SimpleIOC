using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SimpleIOC.Core
{
    public class ConstructorSelector : IConstructorSelector
    {
        ConstructorInfo IConstructorSelector.Select(Type type, Container container)
        {
            return GetGreediestResolvableConstructor(type, container);
        }

        private ConstructorInfo GetGreediestResolvableConstructor(Type type, Container container)
        {
            var constructors = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length);
            foreach (var constructor in constructors)
            {
                if (IsResolvable(constructor, container))
                {
                    return constructor;
                }
            }
            return null;
        }

        private bool IsResolvable(ConstructorInfo constructor, Container container)
        {
            foreach (var param in constructor.GetParameters())
            {
                if (!container.CanResolve(param.ParameterType))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

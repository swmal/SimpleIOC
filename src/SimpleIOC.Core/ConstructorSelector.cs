using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SimpleIOC.Core
{
    public class ConstructorSelector : IConstructorSelector
    {
        private Dictionary<Type, ConstructorInfo> _cachedConstructors = new Dictionary<Type, ConstructorInfo>();
        private readonly object _syncRoot = new object();

        ConstructorInfo IConstructorSelector.Select(Type type, Container container)
        {
            if (!_cachedConstructors.ContainsKey(type))
            {
                return GetGreediestResolvableConstructor(type, container);
            }
            return _cachedConstructors[type];
        }

        private ConstructorInfo GetGreediestResolvableConstructor(Type type, Container container)
        {
            var constructors = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length);
            foreach (var constructor in constructors)
            {
                if (IsResolvable(constructor, container))
                {
                    lock (_syncRoot)
                    {
                        _cachedConstructors[type] = constructor;
                    }
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

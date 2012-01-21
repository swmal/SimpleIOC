using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SimpleIOC.Core.Activation
{
    /// <summary>
    /// This activator is about 20% faster than the <see cref="DefaultTypeActivator"/>, but works only on .NET 40
    /// </summary>
    public class FormatterServicesActivator : DefaultTypeActivator
    {
        private readonly IConstructorSelector _constructorSelector;
        private readonly Container _container;

        public FormatterServicesActivator(Container container, IConstructorSelector constructorSelector)
            : base(container, constructorSelector)
        {
            _constructorSelector = constructorSelector;
            _container = container;
        }

        public override object CreateInstance(Type type)
        {
#if DOTNET40
            var instance = FormatterServices.GetUninitializedObject(type);
            var constructor = _constructorSelector.Select(type, _container);
            var argList = new List<object>();
            foreach (var param in constructor.GetParameters())
            {
                var reg = _container.GetRegistrationOf(param.ParameterType);
                argList.Add(reg.GetImplementationOf());
            }
            constructor.Invoke(instance, argList.ToArray());
            return instance;
#else
            return base.CreateInstance(type);
#endif
        }
    }
}

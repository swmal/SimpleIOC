using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface IContainerBuilder
    {
        IContainer Build();
        ITypeRegistration Register<T>();
        ITypeRegistration Register(Type type);
        IComponentRegistration RegisterComponent<T>(Func<T> factoryMethod) where T : class;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface IContainer
    {
        T Resolve<T>() where T : class;
        object Resolve(Type type);
        T[] ResolveAll<T>() where T : class;
        object[] ResolveAll(Type type);
    }
}

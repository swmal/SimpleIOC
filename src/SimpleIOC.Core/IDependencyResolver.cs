using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface IDependencyResolver
    {
        object Resolve(Type type);
        object Resolve(Type type, string name);
        object[] ResolveAll(Type type);
    }
}

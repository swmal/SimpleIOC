using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface ITypeActivator
    {
        object CreateInstance(Type type);
    }
}

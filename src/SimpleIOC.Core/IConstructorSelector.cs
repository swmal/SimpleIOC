using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SimpleIOC.Core
{
    public interface IConstructorSelector
    {
        ConstructorInfo Select(Type type, Container container);
    }
}

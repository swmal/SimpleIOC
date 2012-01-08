using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface ITypeRegistration : IRegistration
    {
        IRegistration ImplementedBy<T>();
        IRegistration ImplementedBy(Type type);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface IRegistrationStorage
    {
        IRegistration GetOrAdd(IRegistration registration);
        IRegistration GetOrAdd(IRegistration registration, string name);
        IRegistration Get(Type type);
        IRegistration Get(Type type, string name);
    }
}

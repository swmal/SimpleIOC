using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface IRegistration
    {
        Type Type { get; }
        IRegistration WithLifetime(Lifetime lifetime);
        Lifetime Lifetime { get; }
    }
}

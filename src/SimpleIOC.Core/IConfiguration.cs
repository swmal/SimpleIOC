using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface IConfiguration
    {
        IConstructorSelector ConstructorSelector { get; }
        IDependencyResolver DependencyResolver { get; }
        IRegistrationStorage RegistrationStorage { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public interface IConfigurable : IConfiguration
    {
        IConfigurable SetTypeActivator(ITypeActivator typeActivator);
        IConfigurable SetConstructorSelector(IConstructorSelector constructorSelector);
        IConfigurable SetRegistrationStorage(IRegistrationStorage registrationStorage);
        IConfigurable SetDependecyResolver(IDependencyResolver dependencyResolver);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public class DefaultConfiguration : IConfiguration
    {
        private readonly IConstructorSelector _constructorSelector;
        private readonly IDependencyResolver _dependencyResolver;
        private readonly IRegistrationStorage _registrationStorage;

        private DefaultConfiguration(Container container)
        {
            _constructorSelector = new ConstructorSelector();
            _dependencyResolver = new DependencyResolver(this);
            _registrationStorage = new RegistrationStorage();
        }

        public static IConfiguration Create(Container container)
        {
            return new DefaultConfiguration(container);
        }

        public IConstructorSelector ConstructorSelector
        {
            get { return _constructorSelector; }
        }

        public IDependencyResolver DependencyResolver
        {
            get { return _dependencyResolver; }
        }

        public IRegistrationStorage RegistrationStorage
        {
            get { return _registrationStorage; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleIOC.Core.Activation;

namespace SimpleIOC.Core
{
    public class DefaultConfiguration : IConfiguration
    {
        private readonly IConstructorSelector _constructorSelector;
        private readonly IDependencyResolver _dependencyResolver;
        private readonly IRegistrationStorage _registrationStorage;
        private readonly ITypeActivator _typeActivator;

        private DefaultConfiguration(Container container)
        {
            _constructorSelector = new ConstructorSelector();
            _dependencyResolver = new DependencyResolver(this);
            _registrationStorage = new RegistrationStorage();
#if DOTNET40
            _typeActivator = new FormatterServicesActivator(container, _constructorSelector);
#else
            _typeActivator = new DefaultTypeActivator(container, _constructorSelector);
#endif
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

        public ITypeActivator TypeActivator
        {
            get { return _typeActivator; }
        }
    }
}

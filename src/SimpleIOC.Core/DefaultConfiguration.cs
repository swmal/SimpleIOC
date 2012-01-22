using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleIOC.Core.Activation;

namespace SimpleIOC.Core
{
    public class DefaultConfiguration : IConfigurable
    {
        private IConstructorSelector _constructorSelector;
        private IDependencyResolver _dependencyResolver;
        private IRegistrationStorage _registrationStorage;
        private ITypeActivator _typeActivator;

        private DefaultConfiguration(Container container)
        {
            _constructorSelector = new ConstructorSelector();
            _dependencyResolver = new DependencyResolver(this);
            _registrationStorage = new RegistrationStorage();
            _typeActivator = new ExpressionActivator(container, _constructorSelector);
        }

        public static IConfigurable Create(Container container)
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

        IConfigurable IConfigurable.SetTypeActivator(ITypeActivator typeActivator)
        {
            _typeActivator = typeActivator;
            return this;
        }

        IConfigurable IConfigurable.SetConstructorSelector(IConstructorSelector constructorSelector)
        {
            _constructorSelector = constructorSelector;
            return this;
        }

        IConfigurable IConfigurable.SetRegistrationStorage(IRegistrationStorage registrationStorage)
        {
            _registrationStorage = registrationStorage;
            return this;
        }

        IConfigurable IConfigurable.SetDependecyResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
            return this;
        }
    }
}

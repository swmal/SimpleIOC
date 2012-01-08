using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace SimpleIOC.Core
{
    public class Container : IContainer
    {
        private readonly IConfiguration _configuration;

        public Container()
        {
            _configuration = DefaultConfiguration.Create(this);
        }

        public Container(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal ITypeRegistration Register(Type type)
        {
            var registration = new TypeRegistration(type, this);
            return _configuration.RegistrationStorage.GetOrAdd(registration) as ITypeRegistration;
        }

        internal ITypeRegistration Register(Type type, string name)
        {
            var registration = new TypeRegistration(type, this);
            return _configuration.RegistrationStorage.GetOrAdd(registration, name) as ITypeRegistration;
        }

        internal IComponentRegistration RegisterComponent<T>(Func<T> factoryMethod)
            where T : class
        {
            var registration = new ComponentRegistration<T>(factoryMethod, this);
            return _configuration.RegistrationStorage.GetOrAdd(registration) as IComponentRegistration;
        }

        internal bool CanResolve(Type type)
        {
            return _configuration.RegistrationStorage.Get(type) != null;
        }

        public T Resolve<T>()
            where T : class
        {
            return Resolve(typeof(T)) as T;
        }

        public object Resolve(Type type)
        {
            return _configuration.DependencyResolver.Resolve(type);
        }

        public T Resolve<T>(string name)
            where T : class
        {
            var result = _configuration.DependencyResolver.Resolve(typeof(T), name) as T;
            return result;
        }

        public object[] ResolveAll(Type type)
        {
            return _configuration.DependencyResolver.ResolveAll(type);
        }

        public T[] ResolveAll<T>() where T : class
        {
            return _configuration
                        .DependencyResolver
                        .ResolveAll(typeof(T))
                        .Cast<T>()
                        .ToArray();
        }

        internal RegistrationBase GetRegistrationOf(Type type)
        {
            var registration = _configuration.RegistrationStorage.Get(type);
            if (registration == null)
            {
                throw new InvalidOperationException("The requested type [" + type.FullName + "] was not present in the container");
            }
            return (RegistrationBase)registration;
        }
    }
}

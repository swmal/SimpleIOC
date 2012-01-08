using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IConfiguration _configuration;

        public DependencyResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object Resolve(Type type)
        {
            var registration = _configuration.RegistrationStorage.Get(type) as RegistrationBase;
            if (registration == null)
            {
                throw new InvalidOperationException("The requested type [" + type.FullName + "] was not present in the container");
            }
            return registration.GetImplementationOf();
        }


        public object[] ResolveAll(Type type)
        {
            var registration = _configuration.RegistrationStorage.Get(type) as RegistrationBase;
            if (registration == null)
            {
                throw new InvalidOperationException("The requested type [" + type.FullName + "] was not present in the container");
            }
            return registration.GetAllImplementationsOf();
        }
    }
}

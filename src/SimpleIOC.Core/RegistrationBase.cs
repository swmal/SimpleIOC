using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public abstract class RegistrationBase : IRegistration
    {
        public RegistrationBase(Container container)
        {
            Container = container;
        }

        protected Container Container{get; private set; }

        internal abstract object GetImplementationOf();

        internal abstract object[] GetAllImplementationsOf();

        internal abstract IRegistration AddImplementingType(Type implementingType);

        public abstract Type Type { get; }

        public IRegistration WithLifetime(Lifetime lifetime)
        {
            Lifetime = lifetime;
            return this;
        }

        public Lifetime Lifetime
        {
            get;
            private set;
        }
    }
}

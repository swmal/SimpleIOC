using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SimpleIOC.Core.Activation;

namespace SimpleIOC.Core
{
    public class TypeRegistration : RegistrationBase, ITypeRegistration
    {
        private object _instance;
        private Type _type;
        private readonly ITypeActivator _typeActivator;
        private readonly List<Type> _implementingTypes;

        public TypeRegistration(Type type, Container container)
            : this(type, container, new DefaultTypeActivator(container, new ConstructorSelector()))
        {

        }

        public TypeRegistration(Type type, Container container, ITypeActivator typeActivator)
            : base(container)
        {
            _type = type;
            _typeActivator = typeActivator;
            _implementingTypes = new List<Type>();
        }

        IRegistration ITypeRegistration.ImplementedBy<T>()
        {
            return (this as ITypeRegistration).ImplementedBy(typeof(T));
        }

        IRegistration ITypeRegistration.ImplementedBy(Type serviceType)
        {
            if (!Type.IsAssignableFrom(serviceType))
            {
                throw new InvalidOperationException("Implementing type [" + Type.FullName + "] is not assignable from [" + serviceType.FullName + "]");
            }
            _implementingTypes.Add(serviceType);
            return this;
        }

        public override Type Type { get { return _type; } }

        internal IEnumerable<Type> ImplementingTypes { get{ return _implementingTypes; } }

        internal override object GetImplementationOf()
        {
            if (Lifetime == Lifetime.Singleton && _instance != null)
            { 
                return _instance;
            }
            _instance = _typeActivator.CreateInstance(_implementingTypes.First());
            return _instance;
        }

        internal override object[] GetAllImplementationsOf()
        {
            var instances = new List<object>();
            foreach (var type in _implementingTypes)
            {
                instances.Add(_typeActivator.CreateInstance(type));
            }
            return instances.ToArray();

        }

        internal override IRegistration AddImplementingType(Type implementingType)
        {
            _implementingTypes.Add(implementingType);
            return this;
        }
        

    }
}

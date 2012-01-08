using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public class ComponentRegistration<T> : RegistrationBase, IComponentRegistration
        where T : class
    {
        private readonly Func<T> _factoryMethod;
        private T _instance;

        public ComponentRegistration(Func<T> factoryMethod, Container container)
            : base(container)
        {
            _factoryMethod = factoryMethod;
        }

        public override Type Type
        {
            get { return typeof(T); }
        }

        private object GetInstance()
        {
            if (Lifetime == Lifetime.Singleton && _instance != null)
            {
                return _instance;
            }
            _instance = _factoryMethod.Invoke();
            return _instance;
        }

        internal override object GetImplementationOf()
        {
            return GetInstance();
        }

        internal override object[] GetAllImplementationsOf()
        {
            return new List<object> { GetInstance() }.ToArray();
        }

        internal override IRegistration AddImplementingType(Type implementingType)
        {
            throw new NotImplementedException();
        }
    }
}

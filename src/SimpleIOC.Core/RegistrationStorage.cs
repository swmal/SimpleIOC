using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace SimpleIOC.Core
{
    public class RegistrationStorage : IRegistrationStorage
    {
        private readonly ConcurrentDictionary<string, IRegistration> _registrations = new ConcurrentDictionary<string, IRegistration>();

        public IRegistration GetOrAdd(IRegistration registration)
        {
            return GetOrAdd(registration, string.Empty);
        }

        public IRegistration GetOrAdd(IRegistration registration, string name)
        {
            var key = GetRegistrationKey(registration.Type, name);
            return _registrations.GetOrAdd(key, registration);
        }

        public IRegistration Get(Type type)
        {
            return Get(type, string.Empty);
        }

        public IRegistration Get(Type type, string name)
        {
            var key = GetRegistrationKey(type, name);
            if (_registrations.ContainsKey(key))
            {
                return _registrations[key];
            }
            return null;
        }

        private string GetRegistrationKey(Type type, string name)
        {
            return type.FullName + name;
        }


    }
}

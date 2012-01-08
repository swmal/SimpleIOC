using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Core
{
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly Container _container;

        private ContainerBuilder() { }

        private ContainerBuilder(Container container)
        {
            _container = container;
        }

        public static IContainerBuilder CreateBuilder()
        {
            return new ContainerBuilder(new Container());
        }

        ITypeRegistration IContainerBuilder.Register<T>()
        {
            return _container.Register(typeof(T));
        }

        ITypeRegistration IContainerBuilder.Register(Type type)
        {
            return _container.Register(type);
        }

        IComponentRegistration IContainerBuilder.RegisterComponent<T>(Func<T> factoryMethod)
        {
            return _container.RegisterComponent(factoryMethod);
        }

        IContainer IContainerBuilder.Build()
        {
            return _container;
        }
    }
}

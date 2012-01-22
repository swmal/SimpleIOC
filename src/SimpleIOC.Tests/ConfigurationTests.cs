using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIOC.Core;
using SimpleIOC.Tests.TestItems;

namespace SimpleIOC.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        private class TypeActivatorMock : ITypeActivator
        {
            public object CreateInstance(Type type)
            {
                return 123;
            }
        }


        [TestMethod]
        public void TypeActivatorIsConfigurable()
        {

            var typeActivator = new TypeActivatorMock();
            var containerBuilder = ContainerBuilder.CreateBuilder(c =>
            {
                var configuration = DefaultConfiguration.Create(c);
                configuration.SetTypeActivator(typeActivator);
                return configuration;
            });
            containerBuilder.Register<ITestInterface1>().ImplementedBy<TestInterfaceImpl>();
            var container = containerBuilder.Build();
            var result = container.Resolve(typeof(ITestInterface1));
            Assert.AreEqual(123, (int)result);
            
        }
    }
}

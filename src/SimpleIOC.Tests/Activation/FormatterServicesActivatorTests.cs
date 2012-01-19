using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIOC.Core.Activation;
using SimpleIOC.Core;
using SimpleIOC.Tests.TestItems;

namespace SimpleIOC.Tests.Activation
{
    [TestClass]
    public class FormatterServicesActivatorTests
    {
        [TestMethod]
        public void ShouldBuildAnInstanceWithParameterLessConstructor()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<ITestInterface2>().ImplementedBy<TestInterface2Impl>();
            var container = containerBuilder.Build();

            var activator = new FormatterServicesActivator((Container)container, new ConstructorSelector());
            var result = activator.CreateInstance(typeof(TestInterface2Impl));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldBuildAnInstanceWithGreedyConstructor()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<ITestInterface1>().ImplementedBy<TestInterfaceImpl>();
            containerBuilder.Register<ITestInterface2>().ImplementedBy<TestInterface2Impl>();
            containerBuilder.Register<ITestInterface3>().ImplementedBy<TestInterface3Impl>();
            var container = containerBuilder.Build();

            var activator = new FormatterServicesActivator((Container)container, new ConstructorSelector());
            var result = activator.CreateInstance(typeof(TestInterfaceImpl));
            Assert.IsNotNull(result);
        }
    }
}

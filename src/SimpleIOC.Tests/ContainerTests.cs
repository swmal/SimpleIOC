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
    public class ContainerTests
    {
        private IContainer _container;

        [TestInitialize]
        public void TestSetup()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<ITestInterface1>().ImplementedBy<TestInterfaceImpl>();
            containerBuilder.Register<ITestInterface2>().ImplementedBy<TestInterface2Impl>();
            containerBuilder.Register<ITestInterface3>().ImplementedBy<TestInterface3Impl>();
            _container = containerBuilder.Build();
        }

        [TestMethod]
        public void Should_Resolve_Using_Greediest_Constructor()
        {
            var result = _container.Resolve<ITestInterface1>();
            Assert.IsInstanceOfType(result, typeof(TestInterfaceImpl));
            Assert.IsNotNull(result.Test2);

        }

        [TestMethod]
        public void Should_Not_Use_Greediest_Constructor_When_It_Is_Not_Resolvable()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<ITestInterface1>().ImplementedBy<TestInterfaceImpl>();
            containerBuilder.Register<ITestInterface2>().ImplementedBy<TestInterface2Impl>();
            var container = containerBuilder.Build();

            /// Now ITestInterface3 will not be present in the container which will make the container to select the empty constructor.
            var result = container.Resolve<ITestInterface1>();
            Assert.IsNull(result.Test2);
        }

        [TestMethod]
        public void Should_Return_Different_Instances_With_No_Lifetime_Configuration()
        {
            var result = _container.Resolve<ITestInterface1>();
            var result2 = _container.Resolve<ITestInterface1>();

            Assert.AreNotEqual(result, result2);
        }

        [TestMethod]
        public void Should_Return_Same_Instance_With_Singleton_Lifetime_Configuration()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<ITestInterface1>().ImplementedBy<TestInterfaceImpl>().WithLifetime(Lifetime.Singleton);
            containerBuilder.Register<ITestInterface2>().ImplementedBy<TestInterface2Impl>();
            containerBuilder.Register<ITestInterface3>().ImplementedBy<TestInterface3Impl>();
            var container = containerBuilder.Build();
            var result = container.Resolve<ITestInterface1>();
            var result2 = container.Resolve<ITestInterface1>();

            Assert.AreEqual(result, result2);
        }

        [TestMethod]
        public void Should_Return_Same_Instance_With_Singleton_Lifetime_Transient()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<ITestInterface1>().ImplementedBy<TestInterfaceImpl>().WithLifetime(Lifetime.Transient);
            containerBuilder.Register<ITestInterface2>().ImplementedBy<TestInterface2Impl>();
            containerBuilder.Register<ITestInterface3>().ImplementedBy<TestInterface3Impl>();
            var container = containerBuilder.Build();
            var result = container.Resolve<ITestInterface1>();
            var result2 = container.Resolve<ITestInterface1>();

            Assert.AreNotEqual(result, result2);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Should_Throw_An_Exception_When_ServiceType_Is_Not_Assignable_From_Implementing_Type()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<ITestInterface1>().ImplementedBy<TestInterface2Impl>();

        }

        [TestMethod]
        public void Should_Be_Able_To_Assign_A_Type_To_Itself()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<TestInterface2Impl>().ImplementedBy<TestInterface2Impl>();
            var container = containerBuilder.Build();

            Assert.IsInstanceOfType(container.Resolve<TestInterface2Impl>(), typeof(TestInterface2Impl));
        }

        [TestMethod]
        public void Should_Return_Same_Instance_When_RegisterComponent_Has_Lifetime_Singleton()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder
                .RegisterComponent(() => new TestInterface2Impl())
                .WithLifetime(Lifetime.Singleton);
            var container = containerBuilder.Build();
            var expected = container.Resolve<TestInterface2Impl>();
            var result = container.Resolve<TestInterface2Impl>(); ;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Should_Return_Different_Instances_When_RegisterComponent_Has_Lifetime_Transient()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder
                .RegisterComponent(() => new TestInterface2Impl())
                .WithLifetime(Lifetime.Transient);
            var container = containerBuilder.Build();
            var expected = container.Resolve<TestInterface2Impl>();
            var result = container.Resolve<TestInterface2Impl>(); ;
            Assert.AreNotEqual(expected, result);
        }

        [TestMethod]
        public void Should_Return_An_Array_Containing_Two_Items_When_Resolving_All()
        {
            var containerBuilder = ContainerBuilder.CreateBuilder();
            containerBuilder.Register<ITestInterface2>().ImplementedBy<TestInterface2Impl>();
            containerBuilder.Register<ITestInterface2>().ImplementedBy<TestInterface2Impl2>();
            var container = containerBuilder.Build();

            var results = container.ResolveAll(typeof(ITestInterface2));
            Assert.AreEqual(2, results.Count());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIOC.Tests.TestItems
{
    interface ITestInterface1
    {
        int IntValue { get; }
        ITestInterface2 Test2 { get; }
    }

    interface ITestInterface2
    {
    }

    interface ITestInterface3 { }

    class TestInterfaceImpl : ITestInterface1
    {
        public TestInterfaceImpl()
        {

        }

        public TestInterfaceImpl(ITestInterface2 t2, ITestInterface3 t3)
        {
            Test2 = t2;
        }

        public int IntValue
        {
            get { throw new NotImplementedException(); }
        }

        public ITestInterface2 Test2 { get; private set; }
    }

    class TestInterface2Impl : ITestInterface2 { }
    class TestInterface2Impl2 : ITestInterface2 { }
    class TestInterface3Impl : ITestInterface3 { }
}

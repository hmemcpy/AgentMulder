namespace AgentMulder.ReSharper.Tests.Data
{
        public interface IFoo { }
        public interface IBar { }

        public class Foo : IFoo { }
        public class Bar : IBar { }
        public class Baz : IFoo, IBar { }

        public struct MyStruct { }
}
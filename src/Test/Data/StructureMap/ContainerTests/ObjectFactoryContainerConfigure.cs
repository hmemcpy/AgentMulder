using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ObjectFactoryContainerConfigure
    {
        public ObjectFactoryContainerConfigure()
        {
            // ReSharper disable ConvertToLambdaExpression
            ObjectFactory.Container.Configure(x =>
            {
                x.For<IFoo>().Use<Foo>();
            });
            // ReSharper restore ConvertToLambdaExpression
        } 
    }
}
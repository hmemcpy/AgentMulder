using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.Container
{
    public class ObjectFactoryContainerConfigure
    {
        public ObjectFactoryContainerConfigure()
        {
            ObjectFactory.Container.Configure(x =>
            {
                x.For<IFoo>().Use<Foo>();
            });
        } 
    }
}
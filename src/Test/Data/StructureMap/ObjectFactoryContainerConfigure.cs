using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap
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
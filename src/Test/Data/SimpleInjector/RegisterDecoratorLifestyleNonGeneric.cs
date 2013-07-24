// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using SimpleInjector.Extensions;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterDecoratorLifestyleNonGeneric
    {
        public RegisterDecoratorLifestyleNonGeneric()
        {
            var container = new Container();

            container.RegisterDecorator(typeof(ICommon), typeof(CommonImpl1), 
                Lifestyle.CreateHybrid(() => true, Lifestyle.Transient, Lifestyle.Singleton));
        } 
    }
}
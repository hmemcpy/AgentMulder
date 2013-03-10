// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using SimpleInjector.Extensions;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterDecoratorLifestylePredicateNonGeneric
    {
        public RegisterDecoratorLifestylePredicateNonGeneric()
        {
            var container = new Container();

            var custom = Lifestyle.CreateCustom("custom", transientInstanceCreator => () => transientInstanceCreator());

            container.RegisterDecorator(typeof(ICommon), typeof(CommonImpl1), custom, context => true);
        } 
    }
}
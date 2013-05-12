// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector22
{
    public class LifestyleCreateRegistrationGeneric
    {
        public LifestyleCreateRegistrationGeneric()
        {
            var container = new Container();

            Lifestyle lifestyle = Lifestyle.Transient;

            lifestyle.CreateRegistration<ICommon, CommonImpl1>(container);
        } 
    }
}
// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class LifestyleCreateRegistrationNonGeneric
    {
        public LifestyleCreateRegistrationNonGeneric()
        {
            var container = new Container();

            Lifestyle lifestyle = Lifestyle.Transient;

            lifestyle.CreateRegistration(typeof(ICommon), typeof(CommonImpl1), container);
        } 
    }
}
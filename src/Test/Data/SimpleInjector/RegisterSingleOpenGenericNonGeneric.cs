// Patterns: 1
// Matches: Repository.cs
// NotMatches: Foo.cs

using SimpleInjector;
using SimpleInjector.Extensions;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterSingleOpenGenericNonGeneric
    {
        public RegisterSingleOpenGenericNonGeneric()
        {
            var container = new Container();

            container.RegisterSingleOpenGeneric(typeof(IRepository<>), typeof(Repository<,>));
        } 
    }
}
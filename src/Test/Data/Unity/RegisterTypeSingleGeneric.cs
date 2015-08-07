// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeSingleGeneric
    {
        public RegisterTypeSingleGeneric()
        {
            var container = new UnityContainer();
            container.RegisterType<CommonImpl1>();
        }
    }
}
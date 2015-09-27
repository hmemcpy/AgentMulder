// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeNonGeneric
    {
        public RegisterTypeNonGeneric()
        {
            ServiceLocator.Default.RegisterType(typeof(ICommon), typeof(CommonImpl1));
        }
    }
}
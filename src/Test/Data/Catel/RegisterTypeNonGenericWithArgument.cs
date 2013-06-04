// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeNonGenericWithArgument
    {
        public RegisterTypeNonGenericWithArgument()
        {
            ServiceLocator.Instance.RegisterType(typeof(ICommon), typeof(CommonImpl1), false);
        }
    }
}
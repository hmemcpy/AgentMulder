// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeGenericWithArgument
    {
        public RegisterTypeGenericWithArgument()
        {
            ServiceLocator.Instance.RegisterType<ICommon, CommonImpl1>(false);
        } 
    }
}
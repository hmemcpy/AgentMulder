// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Catel.IoC;
using TestApplication.Types;

namespace TestApplication.Catel
{
    public class RegisterTypeIfNotYetRegisteredNonGeneric
    {
        public RegisterTypeIfNotYetRegisteredNonGeneric()
        {
            ServiceLocator.Instance.RegisterTypeIfNotYetRegistered(typeof(ICommon), typeof(CommonImpl1));
        } 
    }
}
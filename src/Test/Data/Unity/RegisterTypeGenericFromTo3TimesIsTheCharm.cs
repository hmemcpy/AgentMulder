// Patterns: 3
// Matches: CommonImpl1.cs,CommonImpl12.cs,Foo.cs
// NotMatches: Bar.cs 

using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeGenericFromTo3TimesIsTheCharm
    {
        public RegisterTypeGenericFromTo3TimesIsTheCharm()
        {
            var container = new UnityContainer();
            container.RegisterType<ICommon, CommonImpl1>()
                .RegisterType<ICommon2, CommonImpl12>()
                .RegisterType<IFoo, Foo>();
        }
    }
}
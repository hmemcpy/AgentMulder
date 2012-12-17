// Patterns: 1
// Matches: MyList.cs
// NotMatches: Foo.cs 

using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeOpenGenericType
    {
        public RegisterTypeOpenGenericType()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(IEnumerable<>), typeof(MyList<>));
        } 
    }
}
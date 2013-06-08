// Patterns: 1
// Matches: CommonImpl1.cs,CommonImpl2.cs
// NotMatches: Foo.cs

using System;
using System.Collections.Generic;
using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterAll3Generic
    {
        public RegisterAll3Generic()
        {
            var container = new Container();

            container.RegisterAll<ICommon>(new List<Type> { typeof(CommonImpl1), typeof(CommonImpl2) });
        } 
    }
}